using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iBike.Data;
using iBike.Resources;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.GPS;

namespace iBike.Controls
{
    public partial class ImportDialog : Form
    {
        public ImportDialog(iBikeActivity activity)
        {
            InitializeComponent();

            UICultureChanged(PluginMain.GetApplication().SystemPreferences.UICulture);

            GlobalSettings settings = new GlobalSettings();

            // Enable/Disable based on track existance
            chkCadence.Enabled = !Utilities.IsNullOrEmpty(activity.CadenceTrack);
            chkDistance.Enabled = !Utilities.IsNullOrEmpty(activity.DistanceMetersTrack);
            chkElevation.Enabled = !Utilities.IsNullOrEmpty(activity.AltitudeTrackM);
            chkHR.Enabled = !Utilities.IsNullOrEmpty(activity.HeartRateTrack);
            chkPower.Enabled = !Utilities.IsNullOrEmpty(activity.PowerTrack);
            chkTilt.Enabled = !Utilities.IsNullOrEmpty(activity.TiltTrack);
            chkWind.Enabled = !Utilities.IsNullOrEmpty(activity.WindTrackKM);
            chkTemp.Enabled = !float.IsNaN(activity.TemperatureCelsius);
            chkGPS.Enabled = activity.GPSRoute != null;
            chkName.Enabled = !String.IsNullOrEmpty(activity.Name);
            chkCalories.Enabled = !float.IsNaN(activity.TotalCalories);

            // Remember previous import settings
            chkCadence.Checked = settings.Cadence && chkCadence.Enabled;
            chkDistance.Checked = settings.Distance && chkDistance.Enabled;
            chkElevation.Checked = settings.Elevation && chkElevation.Enabled;
            chkHR.Checked = settings.HeartRate && chkHR.Enabled;
            chkPower.Checked = settings.Power && chkPower.Enabled;
            chkTilt.Checked = settings.Tilt && chkTilt.Enabled;
            chkWind.Checked = settings.Wind && chkWind.Enabled;
            chkTemp.Checked = settings.Temperature && chkTemp.Enabled;
            chkName.Checked = settings.Name && chkName.Enabled;
            chkGPS.Checked = settings.GPS && chkGPS.Enabled;
            chkCalories.Checked = settings.Calories && chkCalories.Enabled;

            // Initialize display with distance/time info, etc.
            bnrTitle.Text = CommonResources.Text.ActionImport + ": " + activity.StartTime.ToLocalTime().ToShortDateString();
            lblDistTime.Text = activity.Distance.ToString("#.0") + " " + PluginMain.GetApplication().SystemPreferences.DistanceUnits.ToString() + " / " + ToTimeString(activity.Activity.TotalTimeEntered);
            lblStartTime.Text = activity.StartTime.ToLocalTime().DayOfWeek.ToString("G") + " " + activity.StartTime.ToLocalTime().ToShortDateString() + " " + activity.StartTime.ToLocalTime().ToShortTimeString();
            lblRecording.Text = String.Format(Strings.Label_XsecondRecording, activity.RecordInterval);
            lblPoints.Text = String.Format(Strings.Label_XpointsPerTrack, activity.PowerTrack.Count);

            // Populate import data summary items
            lblCadence.Text = GetAvg(activity.CadenceTrack).ToString("0") + " " + CommonResources.Text.LabelRPM;
            lblDistance.Text = activity.Distance.ToString("0") + " " + Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.DistanceUnits);
            double elevation = Length.Convert(GetAvg(activity.AltitudeTrackM), Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits);
            lblElevation.Text = elevation.ToString("0") + " " + Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.ElevationUnits);
            lblHR.Text = GetAvg(activity.HeartRateTrack).ToString("0") + " " + CommonResources.Text.LabelBPM;
            lblPower.Text = GetAvg(activity.PowerTrack).ToString("0") + " " + CommonResources.Text.LabelWatts;
            lblTemp.Text = Temperature.Convert(activity.TemperatureCelsius, Temperature.Units.Celsius, PluginMain.GetApplication().SystemPreferences.TemperatureUnits).ToString("0") + " " + Temperature.LabelAbbr(PluginMain.GetApplication().SystemPreferences.TemperatureUnits);
            lblTilt.Text = GetAvg(activity.TiltTrack).ToString("0.0", CultureInfo.CurrentCulture) + " %";
            double windspeed = Length.Convert(GetAvg(activity.WindTrackKM), Length.Units.Kilometer, PluginMain.GetApplication().SystemPreferences.DistanceUnits);
            lblWind.Text = windspeed.ToString("0.0", CultureInfo.CurrentCulture) + " " + Speed.Label(Speed.Units.Speed, new Length(1, PluginMain.GetApplication().SystemPreferences.DistanceUnits)); // Supposed to output mph as a unit
            lblName.Text = activity.Name;
            lblGPS.Text = GetDistance(activity.GPSRoute) + " " + Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.DistanceUnits);
            lblCalories.Text = activity.TotalCalories.ToString("0") + " " + CommonResources.Text.LabelCalories;
        }

        public void UICultureChanged(CultureInfo culture)
        {
            chkCadence.Text = CommonResources.Text.LabelCadence;
            chkCalories.Text = CommonResources.Text.LabelCalories;
            chkDistance.Text = CommonResources.Text.LabelDistance;
            chkElevation.Text = CommonResources.Text.LabelElevation;
            chkGPS.Text = Strings.Label_GPS;
            chkHR.Text = CommonResources.Text.LabelHeartRate;
            chkName.Text = CommonResources.Text.LabelName;
            chkPower.Text = CommonResources.Text.LabelPower;
            chkTemp.Text = CommonResources.Text.LabelTemperature;
            chkTilt.Text = Strings.Label_Tilt;
            chkWind.Text = Strings.Label_Wind;

            grpUpdate.Text = CommonResources.Text.ActionImport;

            btnCancel.Text = CommonResources.Text.ActionCancel;
            btnFinish.Text = CommonResources.Text.ActionFinish;
        }

        /// <summary>
        /// Returns the average value or 0 if the track is null
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        private float GetAvg(INumericTimeDataSeries track)
        {
            float avgVal = 0;

            if (track != null)
            {
                avgVal = track.Avg;
            }

            return avgVal;
        }

        private string GetDistance(IGPSRoute gpstrack)
        {
            if (gpstrack != null && gpstrack.Count > 0)
            {
                float distance = (float)Length.Convert(gpstrack.TotalDistanceMeters, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits);
                return distance.ToString("0.0", CultureInfo.CurrentCulture);
            }
            else
            {
                return "0";
            }
        }

        internal iBikeActivity.TrackType MatchTrackType
        {
            set
            {
                string label;

                if (value == iBikeActivity.TrackType.Cadence)
                {
                    label = String.Format(Strings.Label_AlignedUsing, CommonResources.Text.LabelCadence);
                }
                else if (value == iBikeActivity.TrackType.Speed)
                {
                    label = String.Format(Strings.Label_AlignedUsing, CommonResources.Text.LabelSpeed);
                }
                else
                {
                    label = String.Format(Strings.Label_AlignedUsing, CommonResources.Text.LabelNone);
                }

                lblAlign.Text = label;
            }
        }

        /// <summary>
        /// Save import settings to GlobalSettings for use next time
        /// </summary>
        private void SaveSettings()
        {
            GlobalSettings settings = new GlobalSettings();

            settings.Cadence = chkCadence.Checked;
            settings.Calories = chkCalories.Checked;
            settings.Distance = chkDistance.Checked;
            settings.Elevation = chkElevation.Checked;
            settings.HeartRate = chkHR.Checked;
            settings.Power = chkPower.Checked;
            settings.Tilt = chkTilt.Checked;
            settings.Wind = chkWind.Checked;
            settings.Temperature = chkTemp.Checked;
            settings.Name = chkName.Checked;
            settings.GPS = chkGPS.Checked;
        }

        /// <summary>
        /// Formats a timespan into hh:mm:ss format.
        /// </summary>
        /// <param name="span">Timespan</param>
        /// <returns>hh:mm:ss formatted string (omits hours if less than 1 hour).  Returns empty string if timespan = 0.</returns>
        private static string ToTimeString(TimeSpan span)
        {
            if (span == TimeSpan.Zero)
            {
                // Return empty if zero.
                return string.Empty;
            }

            string displayTime;

            if ((span.Days * 24) + span.Hours > 0)
            {
                // Hours & minutes
                displayTime = ((span.Days * 24) + span.Hours).ToString("#0", CultureInfo.CurrentCulture) + ":" +
                              span.Minutes.ToString("00") + ":";
            }
            else if (span.Minutes < 10)
            {
                // Single digit minutes
                displayTime = span.Minutes.ToString("#0") + ":";
            }
            else
            {
                // Double digit minutes
                displayTime = span.Minutes.ToString("00") + ":";
            }

            displayTime = displayTime +
                          span.Seconds.ToString("00");

            return displayTime;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }
    }
}