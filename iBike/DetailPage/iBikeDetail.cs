using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using iBike.Data;
using iBike.Resources;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Data.Measurement;

namespace iBike.DetailPage
{
    public partial class iBikeDetail : UserControl
    {
        #region Fields

        private IActivity activity;
        private iBikeActivity iBike;
        private static ChartType chartType = ChartType.Wind;
        private static ChartBasis chartBasis = ChartBasis.Time;

        #endregion

        #region Constructor

        public iBikeDetail()
        {
            InitializeComponent();

            // Set button images
            ZoomChartButton.CenterImage = Resources.Images.ZoomFit;
            ZoomInButton.CenterImage = CommonResources.Images.ZoomIn16;
            ZoomOutButton.CenterImage = CommonResources.Images.ZoomOut16;
            SaveImageButton.CenterImage = CommonResources.Images.Save16;
            ExtraChartsButton.CenterImage = Resources.Images.Charts;
            LeftButton.CenterImage = CommonResources.Images.MoveLeft16;
            RightButton.CenterImage = CommonResources.Images.MoveRight16;

#if Release
            // Hide left/right buttons for release
            this.LeftButton.Visible = false;
            this.RightButton.Visible = false;
            this.importAnalysisToolStripMenuItem.Visible = false;
#endif

            lblTemp.Text = CommonResources.Text.LabelTemperature + " (°" + PluginMain.GetApplication().SystemPreferences.TemperatureUnits.ToString().Substring(0, 1) + ")";

            // Add a Y2Axis
            zedChart.GraphPane.Y2AxisList.Add("Elevation");

            // Add formatted tooltips to graph
            zedChart.PointValueEvent += new ZedGraphControl.PointValueHandler(zedChart_PointValueEvent);
        }

        #endregion

        #region Enumerations

        internal enum ChartBasis
        {
            Time,
            Distance
        }

        internal enum ChartType
        {
            Wind,
            Analysis,
            Tilt
        }

        #endregion

        #region Properties

        /// <summary>
        /// Set the activity to be displayed in the detail pane
        /// </summary>
        public IActivity Activity
        {
            get
            {
                return activity;
            }
            set
            {
                activity = value;
                if (activity != null)
                {
                    iBike = ActivityCache.GetIBikeActivity(activity.ReferenceId);
                }

                // Refresh with new data.  RefreshPage will clear screen if Activity is null
                RefreshPage();
            }
        }

        #endregion

        #region iBike Methods

        internal void RefreshPage()
        {
            if (!iBikePane.Visible) return; // Don't do anything if I'm not visible... duh.

            if (activity == null)
            {
                // Clear the display and exit.
                txtAtm.Text = string.Empty;
                txtClimbing.Text = string.Empty;
                txtEnergy.Text = string.Empty;
                txtFilename.Text = string.Empty;
                txtRecord.Text = string.Empty;
                txtTemp.Text = string.Empty;
                txtWindScale.Text = string.Empty;
                txtWindSpeed.Text = string.Empty;

                zedChart.GraphPane.CurveList.Clear();
                zedChart.Refresh();
                return;
            }

            // Display header data
            txtAtm.Text = iBike.AtmPres.ToString("#.0");
            txtClimbing.Text = Length.Convert(iBike.Climbing, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits).ToString("#.0");
            txtEnergy.Text = iBike.EnergySpent.ToString("0");
            txtFilename.Text = Path.GetFileName(iBike.Filename);
            txtRecord.Text = iBike.RecordInterval.ToString("0");
            txtTemp.Text = Temperature.Convert(activity.Weather.TemperatureCelsius, Temperature.Units.Celsius, PluginMain.GetApplication().SystemPreferences.TemperatureUnits).ToString("#");
            txtWindScale.Text = iBike.WindScaling.ToString("#.000");
            txtWindSpeed.Text = Length.Convert(iBike.WindTrackKM.Avg, Length.Units.Kilometer, activity.Category.DistanceUnits).ToString("#.0") + " " + Speed.Label(Speed.Units.Speed, new Length(1, activity.Category.DistanceUnits));

            switch (chartType)
            {
                case ChartType.Analysis:
                    {
                        // Refresh Analysis data

                        if (Utilities.IsNullOrEmpty(iBike.OriginalTrack) || Utilities.IsNullOrEmpty(iBike.MatchTrack))
                        {
                            // Exit if any of the tracks are null
                            zedChart.GraphPane.CurveList.Clear();
                            zedChart.Refresh();
                            break;
                        }

                        INumericTimeDataSeries originalTrack = new NumericTimeDataSeries(Utilities.RemovePausedTimesInTrack(iBike.OriginalTrack, activity));
                        INumericTimeDataSeries iBikeTrack = new NumericTimeDataSeries(Utilities.RemovePausedTimesInTrack(iBike.MatchTrack, activity));

                        UpdateZedGraphAnalysis(iBikeTrack, originalTrack);

                        break;
                    }
                case ChartType.Wind:
                    {
                        // Display wind chart
                        // Setup data tracks
                        INumericTimeDataSeries speedTrack = Utilities.GetSpeedTrack(activity);
                        INumericTimeDataSeries windTrack = GetRelativeWind(speedTrack, iBike.WindTrackKM);
                        IDistanceDataTrack distanceTrack = Utilities.GetDistanceTrack(activity);

                        // Remove pauses
                        speedTrack = Utilities.RemovePausedTimesInTrack(speedTrack, activity);
                        windTrack = Utilities.RemovePausedTimesInTrack(windTrack, activity);

                        TimeSpan baseTime = new TimeSpan(speedTrack.StartTime.Hour, speedTrack.StartTime.Minute, speedTrack.StartTime.Second);

                        UpdateZedGraphWind(speedTrack, windTrack, distanceTrack);

                        break;
                    }
                case ChartType.Tilt:
                    {
                        // Remove pauses
                        INumericTimeDataSeries tiltTrack = new NumericTimeDataSeries(Utilities.RemovePausedTimesInTrack(iBike.TiltTrack, activity));
                        INumericTimeDataSeries elevTrack = new NumericTimeDataSeries(Utilities.RemovePausedTimesInTrack(iBike.AltitudeTrackM, activity));
                        IDistanceDataTrack distanceTrack = Utilities.GetDistanceTrack(activity);

                        float min, max;
                        int smooth = PluginMain.GetApplication().SystemPreferences.AnalysisSettings.ElevationSmoothingSeconds;
                        tiltTrack = Utilities.STSmooth(tiltTrack, smooth, out min, out max);
                        elevTrack = Utilities.STSmooth(elevTrack, smooth, out min, out max);

                        UpdateZedGraphTilt(tiltTrack, elevTrack, distanceTrack);

                        break;
                    }
            }
        }

        private void UpdateZedGraphWind(INumericTimeDataSeries speedTrack, INumericTimeDataSeries windTrack, IDistanceDataTrack distanceTrack)
        {
            // NOTE: windTrack is RELATIVE WIND
            GraphPane myPane = zedChart.GraphPane;
            Color iBikeWind = Color.FromArgb(90, 0, 128, 192);
            Color speedColor = Utils.Constants.LineChartTypesColors[6];

            // Get points
            PointPairList windPoints;
            PointPairList speedPoints;

            int smoothingSeconds = PluginMain.GetApplication().SystemPreferences.AnalysisSettings.SpeedSmoothingSeconds;

            if (chartBasis == ChartBasis.Distance)
            {
                // Distance base
                Length.Units units = activity.Category.DistanceUnits;
                windPoints = ConvertToDistancePointList(windTrack, distanceTrack, units, smoothingSeconds);
                speedPoints = ConvertToDistancePointList(speedTrack, distanceTrack, units, smoothingSeconds);
                myPane.XAxis.Type = AxisType.Linear;
                myPane.XAxis.Title.Text = CommonResources.Text.LabelDistance + " (" + Length.LabelAbbr(units) + ")";
            }
            else
            {
                // Time base
                windPoints = ConvertToTimePointList(windTrack, smoothingSeconds);
                speedPoints = ConvertToTimePointList(speedTrack, smoothingSeconds);
                myPane.XAxis.Type = AxisType.Date;
                myPane.XAxis.Title.Text = CommonResources.Text.LabelTime;
            }

            // Ensure matching values in each series
            windPoints.Sort(); // Should already be sorted, but it pays to be safe
            speedPoints.Sort(); // Should already be sorted, but it pays to be safe
            for (int i = 0; i < windPoints.Count || i < speedPoints.Count; i++)
            {
                if (windPoints.Count <= i)
                    windPoints.Add(speedPoints[i].X, 0);
                else if (speedPoints.Count <= i)
                    speedPoints.Add(windPoints[i].X, 0);
                else if (windPoints[i].X < speedPoints[i].X)
                    speedPoints.Insert(i, windPoints[i].X, speedPoints.InterpolateX(windPoints[i].X));
                else if (speedPoints[i].X < windPoints[i].X)
                    windPoints.Insert(i, speedPoints[i].X, windPoints.InterpolateX(speedPoints[i].X));
            }
            
            // Store wind values in 'Z' parameter (used for tooltips)
            if (windPoints != null && windPoints.Count > 0)
            {
                double min = windPoints[0].X;
                double max = windPoints[windPoints.Count - 1].X;

                foreach (PointPair point in speedPoints)
                {
                    if (min <= point.X && point.X <= max)
                    {
                        point.Z = windPoints.InterpolateX(point.X);
                    }
                }
            }

            // Store speed values in 'Z' parameter (used for tooltips)
            if (speedPoints != null && speedPoints.Count > 0)
            {
                double min = speedPoints[0].X;
                double max = speedPoints[speedPoints.Count - 1].X;

                foreach (PointPair point in windPoints)
                {
                    if (min <= point.X && point.X <= max)
                    {
                        point.Z = speedPoints.InterpolateX(point.X);
                    }
                }
            }

            // Setup Axis formats and colors etc.
            myPane.YAxis.Title.Text = CommonResources.Text.LabelSpeed;
            myPane.YAxis.Title.FontSpec.FontColor = speedColor;
            myPane.YAxis.Scale.FontSpec.FontColor = speedColor;
            myPane.Y2Axis.IsVisible = false;
            zedChart.GraphPane.CurveList.Clear();

            // Add curves
            LineItem curve;
            curve = myPane.AddCurve("Speed", speedPoints, speedColor, SymbolType.None);
            curve.Line.Width = 1;
            //curve.Line.IsSmooth = true;
            //curve.Line.SmoothTension = .01f;

            curve = myPane.AddCurve("Wind", windPoints, iBikeWind, SymbolType.None);
            //curve.Line.IsSmooth = true;

            // Set fill to fill in area between curves
            curve.Line.Fill = new Fill(curve.Line.Color);

            // Use the stacked curve type so the curve values sum up
            // this also causes only the area between the curves to be filled, rather than
            // the area between each curve and the x axis
            myPane.LineType = LineType.Stack;

            zedChart.AxisChange();

            zedChart.Refresh();

        }

        private void UpdateZedGraphAnalysis(INumericTimeDataSeries iBikeTrack, INumericTimeDataSeries originalTrack)
        {
            GraphPane myPane = zedChart.GraphPane;

            // Find base time
            TimeSpan baseTime;
            if (originalTrack.StartTime < iBikeTrack.StartTime)
            {
                baseTime = originalTrack.StartTime.TimeOfDay;
            }
            else
            {
                baseTime = iBikeTrack.StartTime.TimeOfDay;
            }

            // Get points
            PointPairList iBikePoints = ConvertToTimePointList(iBikeTrack, 5, baseTime);
            PointPairList originalPoints = ConvertToTimePointList(originalTrack, 5, baseTime);

            myPane.XAxis.Title.Text = CommonResources.Text.LabelTime;
            myPane.XAxis.Type = AxisType.Linear;

            switch (iBike.MatchTrackType)
            {
                case iBikeActivity.TrackType.Speed:
                    myPane.YAxis.Title.Text = CommonResources.Text.LabelSpeed;
                    myPane.YAxis.Title.FontSpec.FontColor = Utils.Constants.LineChartTypesColors[6];
                    myPane.YAxis.Scale.FontSpec.FontColor = Utils.Constants.LineChartTypesColors[6];
                    break;
                case iBikeActivity.TrackType.Cadence:
                default:
                    myPane.YAxis.Title.Text = CommonResources.Text.LabelCadence;
                    myPane.YAxis.Title.FontSpec.FontColor = Utils.Constants.LineChartTypesColors[0];
                    myPane.YAxis.Scale.FontSpec.FontColor = Utils.Constants.LineChartTypesColors[0];
                    break;
            }

            zedChart.GraphPane.CurveList.Clear();
            myPane.Y2Axis.IsVisible = false;

            // Add curves
            LineItem curve;
            if (originalPoints.Count > 0)
            {
                curve = myPane.AddCurve("original", originalPoints, Color.Red);
                curve.Symbol.Type = SymbolType.None;
            }

            curve = myPane.AddCurve("iBike", iBikePoints, Color.Green);
            curve.Symbol.Type = SymbolType.None;

            myPane.LineType = LineType.Normal;
            myPane.XAxis.Type = AxisType.Date;

            zedChart.AxisChange();
            zedChart.Refresh();
        }

        private void UpdateZedGraphTilt(INumericTimeDataSeries tiltTrack, INumericTimeDataSeries elevMetersTrack, IDistanceDataTrack distanceTrack)
        {
            GraphPane myPane = zedChart.GraphPane;
            myPane.CurveList.Clear();

            // Get points
            PointPairList elevPoints, tiltPoints;

            int smoothingSeconds = PluginMain.GetApplication().SystemPreferences.AnalysisSettings.ElevationSmoothingSeconds;

            if (chartBasis == ChartBasis.Distance)
            {
                // Distance base
                Length.Units units = activity.Category.DistanceUnits;
                elevPoints = ConvertToDistancePointList(elevMetersTrack, distanceTrack, units, smoothingSeconds);
                tiltPoints = ConvertToDistancePointList(tiltTrack, distanceTrack, units, smoothingSeconds);
                myPane.XAxis.Type = AxisType.Linear;
                myPane.XAxis.Title.Text = CommonResources.Text.LabelDistance + " (" + Length.LabelAbbr(units) + ")";
            }
            else
            {
                // Time base
                elevPoints = ConvertToTimePointList(elevMetersTrack, smoothingSeconds);
                tiltPoints = ConvertToTimePointList(tiltTrack, smoothingSeconds);
                myPane.XAxis.Type = AxisType.Date;
                myPane.XAxis.Title.Text = CommonResources.Text.LabelTime;
            }

            // Convert elevation units
            if (elevPoints != null && elevPoints.Count > 0)
            {
                foreach (PointPair point in elevPoints)
                {
                    point.Y = Length.Convert(point.Y, Length.Units.Meter, activity.Category.ElevationUnits);
                }
            }

            // Setup Y2Axis
            myPane.Y2Axis.IsVisible = true;
            myPane.Y2Axis.Title.FontSpec.FontColor = Utils.Constants.LineChartTypesColors[1];
            myPane.Y2Axis.Scale.FontSpec.FontColor = Utils.Constants.LineChartTypesColors[1];
            myPane.Y2Axis.Title.Text = CommonResources.Text.LabelElevation;

            // Add tilt curve
            LineItem curve;
            curve = myPane.AddCurve("Tilt", tiltPoints, Color.Red, SymbolType.None);
            curve.Line.Fill = new Fill(curve.Line.Color);
            curve.Line.IsSmooth = true;

            // Set axis display options
            myPane.YAxis.Title.Text = CommonResources.Text.LabelGrade + " (%)";
            myPane.YAxis.Title.FontSpec.FontColor = curve.Color;
            myPane.YAxis.Scale.FontSpec.FontColor = curve.Color;
            myPane.LineType = LineType.Normal;

            // Add elevation curve
            curve = myPane.AddCurve("Elevation", elevPoints, Color.Goldenrod, SymbolType.None);
            curve.IsY2Axis = true;

            myPane.Y2Axis.IsVisible = true;
            myPane.Y2Axis.Title.Text = CommonResources.Text.LabelElevation;
            myPane.Y2Axis.Title.FontSpec.FontColor = curve.Color;
            myPane.Y2Axis.Scale.FontSpec.FontColor = curve.Color;

            zedChart.AxisChange();
            zedChart.Refresh();
        }

        /// <summary>
        /// Convert a time track to point pair list that's distance based.
        /// </summary>
        /// <param name="track">Time based input track</param>
        /// <param name="distanceTrack">The distance track used to scale the input track against.  
        /// This correlates time with distance and allows the translation.
        /// The distance track is expected to be in meters.</param>
        /// <param name="units">Desired output distance units</param>
        /// <param name="smooth">Amount of smoothing to apply to input track</param>
        /// <returns>Returns a point pair list (ready for zed graph) that's 
        /// distance based and already converted to the users preferrred distance units.</returns>
        private static PointPairList ConvertToDistancePointList(ITimeDataSeries<float> track, IDistanceDataTrack distanceMetersTrack, Length.Units units, int smooth)
        {
            if (distanceMetersTrack == null) return null;

            PointPairList distancePoints = new PointPairList();
            float max, min;

            track = Utilities.STSmooth(track as NumericTimeDataSeries, smooth, out min, out max);

            foreach (TimeValueEntry<float> point in track)
            {
                DateTime time = track.EntryDateTime(point);
                ITimeValueEntry<float> valueEntry = distanceMetersTrack.GetInterpolatedValue(time);

                if (valueEntry != null)
                {
                    double distance = valueEntry.Value;
                    distance = Length.Convert(distance, Length.Units.Meter, units);
                    distancePoints.Add(distance, point.Value);
                }
            }

            return distancePoints;
        }

        private PointPairList ConvertToTimePointList(INumericTimeDataSeries track, int smooth)
        {
            // Returns a 0 based track
            return ConvertToTimePointList(track, smooth, track.StartTime.TimeOfDay);
        }

        private PointPairList ConvertToTimePointList(INumericTimeDataSeries track, int smooth, TimeSpan adjustment)
        {
            float min, max;

            if (smooth > 1)
            {
                track = Utilities.STSmooth(track, smooth, out min, out max);
            }

            PointPairList points = new PointPairList();

            foreach (TimeValueEntry<float> point in track)
            {
                TimeSpan zone = activity.TimeZoneUtcOffset;
                XDate time = track.StartTime.AddSeconds(point.ElapsedSeconds);
                time.AddSeconds(-adjustment.TotalSeconds);

                points.Add(time, point.Value);
            }

            return points;
        }

        private INumericTimeDataSeries GetRelativeWind(INumericTimeDataSeries speed, INumericTimeDataSeries wind)
        {
            NumericTimeDataSeries relativeWind = new NumericTimeDataSeries();
            DateTime start = wind.StartTime;

            // Stop if bad/empty data
            if (Utilities.IsNullOrEmpty(wind) || Utilities.IsNullOrEmpty(speed)) return relativeWind;

            // Sometimes the data tracks have bad start times and are off by timezone settings.  Can I account for this?
            TimeSpan offset = TimeSpan.Zero;
            TimeSpan threshold = new TimeSpan(0, 20, 0);
            TimeSpan difference = wind.StartTime.Add(activity.TimeZoneUtcOffset) - speed.StartTime;

            // TODO: Attempt at adjusting for bad data.  This might be a dumb idea...
            if (difference < threshold && difference > -threshold)
            {
                // This generally indicates a problem with the activity
                offset = activity.TimeZoneUtcOffset;
            }

            // NOTE: Zed bug... wind chart and speed chart MUST start at the exact same time.  Relative wind will be blank otherwise.
            if (wind.StartTime > speed.StartTime)
            {
                // Add dummy point simply as a placeholder
                relativeWind.Add(speed.StartTime, 0);
            }

            // Calculate delta
            foreach (TimeValueEntry<float> point in wind)
            {
                DateTime time = start.AddSeconds(point.ElapsedSeconds).Add(offset);
                ITimeValueEntry<float> speedPoint = speed.GetInterpolatedValue(time);

                if (speedPoint != null)
                {
                    float speedVal = speedPoint.Value;
                    float delta = point.Value - speedVal;
                    relativeWind.Add(time, delta);
                }
            }

            return relativeWind;
        }

        /// <summary>
        /// Setup zed chart to look like ST charts.  Everything here should be generic (colors, fonts, etc.), not specific to the plugin data.
        /// </summary>
        /// <param name="visualTheme"></param>
        /// <param name="graph"></param>
        private void zedThemeChanged(ITheme visualTheme, ZedGraphControl graph)
        {
            GraphPane myPane = graph.GraphPane;

            // Overall appearance settings
            graph.BorderStyle = BorderStyle.None;
            myPane.Legend.IsVisible = false;
            myPane.Border.IsVisible = false;
            myPane.Title.IsVisible = false;

            // Add a background color
            myPane.Fill.Color = visualTheme.Window;
            myPane.Chart.Fill = new Fill(visualTheme.Window);
            myPane.Chart.Border.IsVisible = false;

            // Add gridlines to the plot, and make them gray
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.DarkGray;
            myPane.YAxis.MajorGrid.Color = myPane.XAxis.MajorGrid.Color;
            myPane.XAxis.MajorGrid.DashOff = 1f;
            myPane.XAxis.MajorGrid.DashOff = myPane.XAxis.MajorGrid.DashOn;
            myPane.XAxis.Scale.MinGrace = 0;
            myPane.XAxis.Scale.MaxGrace = 0;
            myPane.YAxis.MajorGrid.DashOff = myPane.XAxis.MajorGrid.DashOn;
            myPane.YAxis.MajorGrid.DashOff = myPane.YAxis.MajorGrid.DashOn;
            myPane.XAxis.IsAxisSegmentVisible = true;
            myPane.YAxis.IsAxisSegmentVisible = true;

            // Update axis Tic marks
            myPane.XAxis.MinorTic.IsAllTics = false;
            myPane.XAxis.MajorTic.IsAllTics = false;
            myPane.YAxis.MinorTic.IsAllTics = false;
            myPane.YAxis.MajorTic.IsAllTics = false;
            myPane.Y2Axis.MinorTic.IsAllTics = false;
            myPane.Y2Axis.MajorTic.IsAllTics = false;
            myPane.Y2Axis.MajorTic.IsOutside = true;
            myPane.XAxis.MajorTic.IsOutside = true;
            myPane.YAxis.MajorTic.IsOutside = true;
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            myPane.Y2Axis.MajorGrid.IsZeroLine = false;

            // Setup Text Appearance
            string fontName = "Microsoft Sans Sarif";
            myPane.IsFontsScaled = false;
            myPane.XAxis.Title.FontSpec.Family = fontName;
            myPane.XAxis.Title.FontSpec.IsBold = true;
            myPane.XAxis.Scale.FontSpec.Family = fontName;
            myPane.XAxis.Scale.FontSpec.Size = 12;
            myPane.XAxis.Scale.IsUseTenPower = false;

            Color mainCurveColor;
            if (myPane.CurveList.Count > 0)
            {
                mainCurveColor = myPane.CurveList[0].Color;
            }
            else
            {
                mainCurveColor = Color.Black;
            }

            myPane.YAxis.Title.FontSpec.Family = fontName;
            myPane.YAxis.Title.FontSpec.IsBold = true;
            myPane.YAxis.Title.FontSpec.FontColor = mainCurveColor;
            myPane.YAxis.Scale.FontSpec.Family = fontName;
            myPane.YAxis.Scale.FontSpec.Size = 12;

            zedChart.Refresh();
        }

        #endregion

        #region Misc Support

        /// <summary>
        /// Update visual theme
        /// </summary>
        /// <param name="visualTheme"></param>
        public void ThemeChanged(ITheme visualTheme)
        {
            ButtonPanel.ThemeChanged(visualTheme);
            ButtonPanel.BackColor = visualTheme.Window;
            ChartBanner.ThemeChanged(visualTheme);
            panelMain.ThemeChanged(visualTheme);
            panelMain.BackColor = visualTheme.Window;
            panelTop.BackColor = visualTheme.Control;
            zedThemeChanged(visualTheme, zedChart);
            txtFilename.ThemeChanged(visualTheme);
            txtEnergy.ThemeChanged(visualTheme);
            txtClimbing.ThemeChanged(visualTheme);
            txtAtm.ThemeChanged(visualTheme);
            txtClimbing.ThemeChanged(visualTheme);
            txtRecord.ThemeChanged(visualTheme);
            txtTemp.ThemeChanged(visualTheme);
            txtWindScale.ThemeChanged(visualTheme);
            txtWindSpeed.ThemeChanged(visualTheme);
        }

        /// <summary>
        /// Localize Display
        /// </summary>
        /// <param name="culture"></param>
        public void UICultureChanged(CultureInfo culture)
        {
            zedChart.GraphPane.YAxis.Title.Text = CommonResources.Text.LabelSpeed;
            zedChart.GraphPane.XAxis.Title.Text = CommonResources.Text.LabelTime;
            windSpeedDistance.Text = Strings.Label_Wind + " / " + CommonResources.Text.LabelDistance;
            windSpeedTime.Text = Strings.Label_Wind + " / " + CommonResources.Text.LabelTime;
            tiltDistance.Text = Strings.Label_Tilt + " / " + CommonResources.Text.LabelDistance;
            tiltTime.Text = Strings.Label_Tilt + " / " + CommonResources.Text.LabelTime;

            lblPres.Text = Strings.Label_ATMPress;
            lblClimbing.Text = CommonResources.Text.LabelClimb + " (" + Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.ElevationUnits) + ")";
            lblEnergy.Text = CommonResources.Text.LabelEnergyKJ;
            lblFilename.Text = CommonResources.Text.LabelFilename;
            lblRecInt.Text = Strings.Label_RecordInterval;
            lblTemp.Text = CommonResources.Text.LabelTemperature + " (" + Temperature.LabelAbbr(PluginMain.GetApplication().SystemPreferences.TemperatureUnits) + ")";
            lblWindScale.Text = Strings.Label_WindScale;
            lblWindSpeed.Text = Strings.Label_WindSpeed;

            ChartBanner.Text = Strings.Label_iBike;
        }

        #endregion

        #region Handlers

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in MenuStrip.Items)
            {
                item.Checked = false;
            }

            ToolStripMenuItem selected = sender as ToolStripMenuItem;
            selected.Checked = true;

            switch (selected.Name)
            {
                case "windSpeedTime":
                    chartType = ChartType.Wind;
                    chartBasis = ChartBasis.Time;
                    break;
                case "tiltTime":
                    chartType = ChartType.Tilt;
                    chartBasis = ChartBasis.Time;
                    break;
                case "windSpeedDistance":
                    chartType = ChartType.Wind;
                    chartBasis = ChartBasis.Distance;
                    break;
                case "tiltDistance":
                    chartType = ChartType.Tilt;
                    chartBasis = ChartBasis.Distance;
                    break;
                case "importAnalysisToolStripMenuItem":
                    chartType = ChartType.Analysis;
                    chartBasis = ChartBasis.Time;
                    break;
            }

            RefreshPage();
        }

        private void ChartBanner_MenuClicked(object sender, EventArgs e)
        {
            ActionBanner banner = sender as ActionBanner;

            if (banner != null)
            {
                ZoneFiveSoftware.Common.Visuals.Button button = banner.Controls[0] as ZoneFiveSoftware.Common.Visuals.Button;
                Point point = banner.PointToScreen(button.Location);
                point.X = point.X - MenuStrip.Width + button.Width;
                point.Y = point.Y + button.Height;

                MenuStrip.Show(point);
            }
        }

        private void ClearDataButton_Click(object sender, EventArgs e)
        {
            // TODO: Clear button was removed.
            if (activity != null)
            {
                byte[] empty = new byte[] { };
                activity.SetExtensionData(GUIDs.PluginMain, empty);
                ActivityCache.PurgeActivity(activity);
            }
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            iBike.ShiftUpdatedTracks(10);

            RefreshPage();
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            iBike.ShiftUpdatedTracks(-10);

            RefreshPage();
        }

        private void ExtraChartsButton_Click(object sender, EventArgs e)
        {

        }

        private void ZoomButton_Click(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.Button button = sender as ZoneFiveSoftware.Common.Visuals.Button;
            if (button == null) return;

            float zoomFraction = .1f;

            if (button.Tag as string == "Out")
            {
                zoomFraction = 1 + zoomFraction;
            }
            else
            {
                zoomFraction = 1 - zoomFraction;
            }

            zedChart.ZoomPane(zedChart.GraphPane, zoomFraction, new PointF(), false);
        }

        private void ZoomChartButton_Click(object sender, EventArgs e)
        {
            zedChart.ZoomOutAll(zedChart.GraphPane);
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Formats the tooltip popups
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="pane">The parameter is not used.</param>
        /// <param name="curve">The curve containing the points</param>
        /// <param name="iPt">The index of the point of interest</param>
        /// <returns>A tooltip string</returns>
        private string zedChart_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            string tooltip = string.Empty;
            XDate date = curve[iPt].X;
            TimeSpan time = date.DateTime.TimeOfDay;
            string format = "{0}\r\n{1}: {2} {3}";
            double speed, wind;

            string basis;
            if (chartBasis == ChartBasis.Time)
            {
                basis = time.ToString();
            }
            else
            {
                basis = curve[iPt].X.ToString("0.#") + " " + Length.LabelAbbr(activity.Category.DistanceUnits);
            }

            switch (chartType)
            {
                case ChartType.Analysis:
                    tooltip = string.Format(format, basis, CommonResources.Text.LabelRPM, curve[iPt].Y.ToString("0.#"), "");
                    break;

                case ChartType.Tilt:
                    if (curve.Label.Text == "Elevation")
                    {
                        tooltip = string.Format(format, basis, CommonResources.Text.LabelElevation, curve[iPt].Y.ToString("0.#"), Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.ElevationUnits));
                    }
                    else if (curve.Label.Text == "Tilt")
                    {
                        tooltip = string.Format(format, basis, Strings.Label_Tilt, curve[iPt].Y.ToString("0.#"), "%");
                    }
                    break;

                case ChartType.Wind:
                    if (curve.Label.Text == "Wind")
                    {
                        speed = curve[iPt].Z;
                        wind = curve[iPt].Y;
                    }
                    else
                    {
                        speed = curve[iPt].Y;
                        wind = curve[iPt].Z;
                    }

                    format += "\r\n{4}: {5} {3}";
                    string mph = Speed.Label(Speed.Units.Speed, new Length(1, activity.Category.DistanceUnits));
                    tooltip = string.Format(format, basis, CommonResources.Text.LabelSpeed, speed.ToString("0.#"), mph, Strings.Label_Wind, wind.ToString("0.#"));
                    break;
            }
            return tooltip;
        }
        #endregion

        private void btnUpgrade_Click(object sender, EventArgs e)
        {

        }
    }
}
