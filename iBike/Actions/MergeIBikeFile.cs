using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using iBike.Data;
using iBike.Importer;
using iBike.Resources;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;

namespace iBike.Actions
{
    class MergeIBikeFile : IAction
    {
        #region Fields

        private static IActivity activity;
        private static IDailyActivityView dailyActivityView;
        private static IActivityReportsView activityReportView;
        private static float thresholdSpeed = 1.1176f; // meters/second
        private static float thresholdTime = 5; // Consecutive seconds below speed for iBike to stop
        private bool manual;
        private string filename;

        #endregion

        #region Constructors

        internal MergeIBikeFile(IDailyActivityView view, bool manual)
        {
            this.manual = manual;
            MergeIBikeFile.dailyActivityView = view;
        }

        internal MergeIBikeFile(IActivityReportsView view, bool manual)
        {
            this.manual = manual;
            MergeIBikeFile.activityReportView = view;
        }

        #endregion

        #region Properties

        internal IActivity Activity
        {
            set { activity = value; }
        }

        #endregion

        #region IAction Members

        public bool Enabled
        {
            get
            {
                if (manual || !string.IsNullOrEmpty(GetiBikeFile(activity)))
                {
                    // Note that this is only called when an activity is selected because menu is not Visible if activity==null
                    return true;
                }

                return false;
            }
        }

        public IList<string> MenuPath
        {
            get
            {
                return new List<string> { Strings.Label_iBike };
            }
        }

        public bool Visible
        {
            get
            {
                // Logbook check is used here because this is called many times during ST startup, and .ActiveView throws NullRef exception otherwise.
                if (PluginMain.GetApplication().Logbook != null)
                {
                    // Get selected activity
                    if (PluginMain.GetApplication().ActiveView.Id == GUIDs.ActivityReportsView)
                    {
                        Activity = CollectionUtils.GetSingleItemOfType<IActivity>(activityReportView.SelectionProvider.SelectedItems);
                    }
                    else if (PluginMain.GetApplication().ActiveView.Id == GUIDs.DailyActivityView)
                    {
                        Activity = CollectionUtils.GetSingleItemOfType<IActivity>(dailyActivityView.SelectionProvider.SelectedItems);
                    }
                    else
                    {
                        // Should never hit...?
                        return false;
                    }

                    // Determine whether to show or not
                    return (activity != null);
                }
                else
                {
                    // Nothing loaded
                    return false;
                }
            }
        }

        public bool HasMenuArrow
        {
            get
            {
                // Does nothing as an Edit menu action item.
                return false;
            }
        }

        public Image Image
        {
            get { return Resources.Images.iBikeWin24; }
        }

        public void Refresh()
        {

        }

        public void Run(Rectangle rectButton)
        {
            iBikeActivity iBikeAct = MergeIBikeActivity(activity, filename);

            if (iBikeAct != null)
            {
                ActivityCache.AddIBikeActivity(iBikeAct, true);
            }

            iBike.DetailPage.iBikePane.Instance.Activity = activity;
        }

        public string Title
        {
            get
            {
                if (manual)
                {
                    return Strings.Action_MergeiBikeFile + "...";
                }
                else
                {
                    filename = GetiBikeFile(activity);

                    if (string.IsNullOrEmpty(filename))
                    {
                        // Shouldn't be visible in this case
                        return Strings.Text_NoMatchingFileFound;
                    }
                    else
                    {
                        return Strings.Label_Merge + ": " + filename;
                    }
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Merge/update ST activity with data from an iBike file
        /// </summary>
        /// <param name="activity">Activity to update</param>
        internal static iBikeActivity MergeIBikeActivity(IActivity activity, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                // Open file dialog (manual import)
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = GlobalSettings.Instance.Path;

                DialogResult result = dlg.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return null;
                }

                filename = dlg.FileName;
            }
            else
            {
                // Attempto to open semi-auto file
                filename = Path.Combine(GlobalSettings.Instance.Path, filename);
            }

            try
            {
                // Read file
                iBikeActivity iBike = FileImporter.ReadIBikeFile(filename);

                // Merge with existing activity
                SyncWithActivity(activity, ref iBike);
                return UpdateActivityData(iBike, activity);
            }
            catch (Exception ex)
            {
                MessageDialog.Show(Strings.Text_ImportError + Environment.NewLine + ex.Message, Strings.Label_iBikeImport, MessageBoxButtons.OK);
                return null;
            }
        }

        /// <summary>
        /// Write data from iBike activity to ST activity
        /// </summary>
        /// <param name="iBike">Populated iBike activity</param>
        /// <param name="activity">Activity to add/replace data in</param>
        internal static iBikeActivity UpdateActivityData(iBikeActivity iBikeAct, IActivity activity)
        {
            // Specify which tracks are updated
            Controls.ImportDialog importDlg = new Controls.ImportDialog(iBikeAct);

            importDlg.MatchTrackType = iBikeAct.MatchTrackType;

            importDlg.ShowDialog();
            if (importDlg.DialogResult != DialogResult.OK)
            {
                return null;
            }

            // Update power track
            if (importDlg.chkPower.Checked && !Utilities.IsNullOrEmpty(iBikeAct.PowerTrack))
            {
                activity.PowerWattsTrack = iBikeAct.PowerTrack;
            }

            if (importDlg.chkHR.Checked && !Utilities.IsNullOrEmpty(iBikeAct.HeartRateTrack))
            {
                activity.HeartRatePerMinuteTrack = iBikeAct.HeartRateTrack;
            }

            if (importDlg.chkCadence.Checked && !Utilities.IsNullOrEmpty(iBikeAct.CadenceTrack))
            {
                activity.CadencePerMinuteTrack = iBikeAct.CadenceTrack;
            }

            if (importDlg.chkDistance.Checked && !Utilities.IsNullOrEmpty(iBikeAct.DistanceMetersTrack))
            {
                activity.DistanceMetersTrack = iBikeAct.DistanceMetersTrack;
            }

            // Only import elevation if NOT importing GPS route
            if (!importDlg.chkGPS.Checked && importDlg.chkElevation.Checked && !Utilities.IsNullOrEmpty(iBikeAct.AltitudeTrackM))
            {
                if (activity.GPSRoute == null || activity.GPSRoute.TotalDistanceMeters < 15)
                {
                    // Set stand-alone elevation track
                    activity.ElevationMetersTrack = iBikeAct.AltitudeTrackM;
                }
                else
                {
                    // Merges elevation into existing gps track (elevation track must be within bounds of GPS track
                    for (int index = 0; index < activity.GPSRoute.Count; index++)
                    {
                        ITimeValueEntry<IGPSPoint> activityPoint = activity.GPSRoute[index];
                        DateTime time = activity.GPSRoute.EntryDateTime(activityPoint);
                        ITimeValueEntry<float> iBikePoint = iBikeAct.AltitudeTrackM.GetInterpolatedValue(time);

                        if (iBikePoint != null)
                        {
                            IGPSPoint point = new GPSPoint(activityPoint.Value.LatitudeDegrees, activityPoint.Value.LongitudeDegrees, iBikePoint.Value);
                            activity.GPSRoute.SetValueAt(index, point);
                        }
                    }
                }

                activity.TotalAscendMetersEntered = iBikeAct.Climbing;
                activity.TotalDescendMetersEntered = -activity.TotalAscendMetersEntered;
            }

            if (importDlg.chkName.Checked && !string.IsNullOrEmpty(importDlg.Name))
            {
                activity.Name = iBikeAct.Name;
            }

            if (importDlg.chkGPS.Checked)
            {
                activity.GPSRoute = iBikeAct.GPSRoute;
            }

            if (importDlg.chkTemp.Checked)
            {
                activity.Weather.TemperatureCelsius = iBikeAct.TemperatureCelsius;
            }

            if (importDlg.chkCalories.Checked)
            {
                activity.TotalCalories = iBikeAct.TotalCalories;
            }

            // TODO: Decide how to merge laps if activity already has lap info
            if (activity.Laps == null || activity.Laps.Count <= 1)
            {
                // Replace laps
                activity.Laps.Clear();
                foreach (KeyValuePair<DateTime, TimeSpan> lap in iBikeAct.Laps)
                {
                    activity.Laps.Add(lap.Key, lap.Value);
                }
            }

            // Append to activity notes:
            //  08/28/2010 - Imported with iBike plugin ver. 0.5: iBike_2010_08_25_1845.csv
            activity.Notes = DateTime.Now.ToLocalTime().ToShortDateString() + " - " + String.Format(Strings.Text_ActivityNote, new PluginMain().Version, Path.GetFileName(iBikeAct.Filename)) 
                            + Environment.NewLine + iBikeAct.Notes
                            + Environment.NewLine 
                            + Environment.NewLine + activity.Notes;

            // Store Wind & Tilt Track
            byte[] data = iBikeAct.GetSerializedStream();
            activity.SetExtensionData(GUIDs.PluginMain, data);

            return iBikeAct;
        }

        /// <summary>
        /// Get a list of filenames that match the activity date
        /// </summary>
        /// <param name="activity">Activity to match</param>
        /// <returns>List of possible </returns>
        internal static string GetiBikeFile(IActivity activity)
        {
            if (activity == null || string.IsNullOrEmpty(GlobalSettings.Instance.Path) || !Directory.Exists(GlobalSettings.Instance.Path)) return null;

            List<string> filenames = new List<string>();
            string filename = string.Empty;

            int index = GlobalSettings.Instance.DateIndex;
            int y = 0, m = 0, d = 0;
            int t = index + 3;

            DateTime date = activity.StartTime.Add(activity.TimeZoneUtcOffset);

            // Set where in filename to look for date strings
            switch (GlobalSettings.Instance.DateStyle)
            {
                case GlobalSettings.DateFormat.YYYY_MM_DD:
                    y = index;
                    m = index + 1;
                    d = index + 2;
                    break;
                case GlobalSettings.DateFormat.MM_DD_YYYY:
                    y = index + 2;
                    m = index;
                    d = index + 1;
                    break;
                case GlobalSettings.DateFormat.DD_MM_YYYY:
                    y = index + 2;
                    m = index + 1;
                    d = index;
                    break;
            }

            // Look through filenames for matching dates (filenames)
            foreach (string file in Directory.GetFiles(GlobalSettings.Instance.Path))
            {
                string currentFile = Path.GetFileName(file);

                if (Path.GetExtension(file).ToLower(CultureInfo.InvariantCulture) == ".csv")
                {
                    string[] parts = currentFile.Split('_');

                    if (parts.Length > Math.Max(y, Math.Max(m, d)))
                    {
                        // See if filename matches date
                        if (parts[y] == date.Year.ToString("0000", CultureInfo.InvariantCulture) && parts[m] == date.Month.ToString("00", CultureInfo.InvariantCulture) && parts[d] == date.Day.ToString("00", CultureInfo.InvariantCulture))
                        {
                            // currentFile is just the filename... path has been excluded
                            filenames.Add(currentFile);
                        }
                    }
                }
            }

            // Find closest matching time
            TimeSpan closest = TimeSpan.MaxValue;

            foreach (string file in filenames)
            {
                string[] parts = file.Split('_');
                TimeSpan time;

                if (parts.Length > t)
                {
                    // Parse time string (it's always right after the date)
                    int hr, min;
                    if (int.TryParse(parts[t].Substring(0, 2), out hr) && int.TryParse(parts[t].Substring(2, 2), out min))
                    {
                        time = new TimeSpan(hr, min, 0);
                        if ((date.TimeOfDay - time).Duration() < closest)
                        {
                            // Better match found
                            closest = (date.TimeOfDay - time).Duration();
                            filename = file;
                        }
                    }
                }
            }

            return filename;
        }

        #region Sync support routines

        /// <summary>
        /// Adjust imported iBike activity to match existing activity
        /// (Original used in FileImporter)
        /// </summary>
        /// <param name="activity">Existing activity</param>
        /// <param name="importData">New iBike activity</param>
        internal static void SyncWithActivity(IActivity activity, ref iBikeActivity importData)
        {
            iBikeActivity oldiBike = ActivityCache.GetIBikeActivity(activity.ReferenceId);
            importData.ReferenceId = activity.ReferenceId;
            importData.ImportStartTime = activity.StartTime;

            // Align to existing alignment track.  Intention here is for subsequent imports not to add 
            //     further error to alignment.  Looking for exact same results on re-import.
            if (!Utilities.IsNullOrEmpty(oldiBike.OriginalTrack) && !Utilities.IsNullOrEmpty(oldiBike.MatchTrack))
            {
                INumericTimeDataSeries iBikeTrack;

                // Retain original track settings in new activity
                importData.OriginalTrack = oldiBike.OriginalTrack;
                importData.MatchTrackType = oldiBike.MatchTrackType;

                switch (oldiBike.MatchTrackType)
                {
                    case iBikeActivity.TrackType.Cadence:
                        iBikeTrack = importData.CadenceTrack;
                        break;
                    case iBikeActivity.TrackType.Speed:
                        // TODO: (MED) Import sync using speed may be broken due to changed unit storage (mph vs m/s)?  untested.
                        iBikeTrack = Utilities.GetSpeedTrack(importData.DistanceMetersTrack);
                        break;
                    case iBikeActivity.TrackType.HeartRate:
                        iBikeTrack = importData.HeartRateTrack;
                        break;
                    default:
                        iBikeTrack = new NumericTimeDataSeries();
                        break;
                }

                importData = AlignToDataTrack(oldiBike.OriginalTrack, iBikeTrack, activity, importData);
                return;
            }

            // Align to cadence track if possible
            if (!Utilities.IsNullOrEmpty(activity.CadencePerMinuteTrack) && !Utilities.IsNullOrEmpty(importData.CadenceTrack))
            {
                importData = AlignToDataTrack(activity.CadencePerMinuteTrack, importData.CadenceTrack, activity, importData);
                importData.OriginalTrack = new NumericTimeDataSeries(activity.CadencePerMinuteTrack);
                importData.MatchTrackType = iBikeActivity.TrackType.Cadence;
                return;
            }

            // Align using (smoothed?) speed track
            INumericTimeDataSeries activitySpeedTrack = Utilities.GetSpeedTrack(activity);
            INumericTimeDataSeries iBikeSpeedTrack = Utilities.GetSpeedTrack(importData.DistanceMetersTrack);

            if (!Utilities.IsNullOrEmpty(activitySpeedTrack) && !Utilities.IsNullOrEmpty(iBikeSpeedTrack))
            {
                importData = AlignToDataTrack(activitySpeedTrack, iBikeSpeedTrack, activity, importData);
                importData.OriginalTrack = new NumericTimeDataSeries(activitySpeedTrack);
                importData.MatchTrackType = iBikeActivity.TrackType.Speed;
                return;
            }

            // Align using Heartrate
            if (!Utilities.IsNullOrEmpty(activity.HeartRatePerMinuteTrack) && !Utilities.IsNullOrEmpty(importData.HeartRateTrack))
            {
                importData = AlignToDataTrack(activity.HeartRatePerMinuteTrack, importData.HeartRateTrack, activity, importData);
                importData.OriginalTrack = new NumericTimeDataSeries(activity.HeartRatePerMinuteTrack);
                importData.MatchTrackType = iBikeActivity.TrackType.HeartRate;
                return;
            }

            // Last ditch effort: Align using start times
            // This is the least accurate method?  Almost guaranteed for there to be an offset of some sort.
            importData.StartTime = activity.StartTime;

            //ExportTracks(activity.CadencePerMinuteTrack, importData.CadenceTrack, "C:\\ST exports\\iBike\\" + importData.StartTime.Year + "_" + importData.StartTime.Month + "_" + importData.StartTime.Day + "CAD.csv");
        }

        /// <summary>
        /// Fully align/sync iBike track with existing data track.  
        /// 1 - Adjust StartTime
        /// 2 - Insert Pauses as they're found
        /// 3 - Sync segment
        /// 4 - GoTo #2, repeat.
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="iBikeTrack"></param>
        /// <param name="activity"></param>
        /// <param name="importResults"></param>
        /// <returns></returns>
        private static iBikeActivity AlignToDataTrack(INumericTimeDataSeries existing, INumericTimeDataSeries iBikeTrack, IActivity activity, iBikeActivity importResults)
        {
            // NOTE: This is where a lot of the alignment magic happens...
            // NOTE: that segStart/End refer to times in existing track so they don't move around as iBikeTrack is being shifted
            DateTime segStart, segEnd;
            ValueRange<DateTime> pause, nextPause;
            int offset, bias;
            segStart = existing.StartTime;
            pause = GetNextPause(activity, segStart);
            if (pause != null)
            {
                segEnd = pause.Lower;
            }
            else
            {
                segEnd = existing.EntryDateTime(existing[existing.Count - 1]);
            }

            // Adjust initial starttime
            offset = GetSegmentOffset(existing, iBikeTrack, segStart, segEnd, 0);
            DateTime start = iBikeTrack.StartTime.AddSeconds(offset);
            importResults.StartTime = start;
            iBikeTrack = Utilities.SetTrackStart(iBikeTrack, start) as INumericTimeDataSeries;

#if Debug
            if (pause != null)
            {
                System.Diagnostics.Debug.WriteLine("---  Activity: " + activity.StartTime + "  ---");
                System.Diagnostics.Debug.WriteLine("Active Segment: " + (existing.StartTime - segStart).Duration() + ", " + (existing.StartTime - segEnd).Duration() + ", " + 0 + ", " + 0 + ", New Pause: " + (existing.StartTime - pause.Lower).Duration() + ", " + (existing.StartTime - pause.Upper).Duration());
            }
#endif
            while (pause != null)
            {
                segStart = pause.Upper;
                nextPause = GetNextPause(activity, segStart);

                if (nextPause == null)
                {
                    // Set to end of track
                    segEnd = existing.StartTime.AddSeconds(existing.TotalElapsedSeconds);
                }
                else
                {
                    segEnd = nextPause.Lower;
                }

                // Bias is the pause duration.  This is how much we -guess- the iBike track to be offset by.  
                //  From there, we do square root stuff to fine tune it a few seconds one way or the other.
                bias = GetBias(pause);

                // Offset is the final amount of time to insert into the iBike track.
                offset = GetSegmentOffset(existing, iBikeTrack, segStart, segEnd, bias);

                // Modify pause before inserting to get a better fit
                pause = new ValueRange<DateTime>(pause.Lower, pause.Upper.AddSeconds(offset));
#if Debug
                //System.Diagnostics.Debug.WriteLine("Pause: " + nextPause.Lower + ", " + nextPause.Upper + ", " + bias + ", " + offset + ", New Pause: " + pause.Lower + ", " + pause.Upper);
                System.Diagnostics.Debug.WriteLine("Active Segment: " + (existing.StartTime - segStart).Duration() + ", " + (existing.StartTime - segEnd).Duration() + ", " + bias + ", " + offset + ", New Pause: " + (existing.StartTime - pause.Lower).Duration() + ", " + (existing.StartTime - pause.Upper).Duration());
#endif
                // Insert pauses
                importResults.AddPause(pause);
                iBikeTrack = Utilities.InsertPause(iBikeTrack, pause) as INumericTimeDataSeries;

                pause = nextPause;
            }

            return importResults;
        }

        /// <summary>
        /// Align start times of activities.  Using a sum of squares comparison, it will compare 2 data tracks and attempt to align them.
        /// </summary>
        /// <param name="existing">Existing data track (think Garmin cadence track)</param>
        /// <param name="iBikeTrack">New iBike data track (matching cadence track for instance)</param>
        /// <param name="segStart"></param>
        /// <param name="segEnd"></param>
        /// <param name="biasSeconds">Seconds to bias existing track for calculation.  Useful for accounting for a pause.  Enter 0 for no bias.</param>
        /// <returns></returns>
        private static int GetSegmentOffset(INumericTimeDataSeries existing, INumericTimeDataSeries iBikeTrack, DateTime segStart, DateTime segEnd, int biasSeconds)
        {
            double sumSquares = 0;
            double minSquares = double.MaxValue;
            int count = 0;
            int bestOffset = 0;

            // Plus/minus search value
            // TODO: Set range really tight for mid-activity adjustments.
            int range = 120;
            range = Math.Max(Math.Min(range, (int)(segEnd - segStart).TotalSeconds - 15), 1);

            if (iBikeTrack != null && segEnd > iBikeTrack.EntryDateTime(iBikeTrack[iBikeTrack.Count - 1]))
            {
                segEnd = iBikeTrack.EntryDateTime(iBikeTrack[iBikeTrack.Count - 1]);
            }

            // *******************************************************
            // Assume the recording devices are synced pretty close...
            // clocks are within 2 minutes of one another
            // *******************************************************
            for (int offset = -range - biasSeconds; offset < range - biasSeconds; offset++)
            {
                // Could look for 50% dip as sign for matching?... not currently implemented.
                //  Edit: No need, the current algo is fast and works well.
                foreach (TimeValueEntry<float> point in existing)
                {
                    DateTime pointTime = existing.EntryDateTime(point);

                    // Valid point times: after iBike start,
                    //      before end of existing track or start of pause
                    //      before end of iBikeTrack
                    if (pointTime > segStart && pointTime < segEnd)
                    {
                        // Sum the squares of the error, then divide by n samples and take square root of this
                        // Minimize this number
                        // Note that this is where the bias is applied.
                        pointTime = pointTime.AddSeconds(offset);
                        ITimeValueEntry<float> iBikePoint = iBikeTrack.GetInterpolatedValue(pointTime);

                        if (iBikePoint != null)
                        {
                            sumSquares += Math.Pow((point.Value - iBikePoint.Value), 2);
                            count++;
                        }
                        else
                        {
                            // Crap... shouldn't happen
                        }
                    }
                    else if (pointTime > segEnd)
                    {
                        // End of evaluation
                        break;
                    }
                    else
                    {
                        // Point outside of bounds... next point
                        // Could occur at beginning or end of data track
                    }
                }

                // I don't really know what I'm doing here (don't know the *real* formula), but I think it works pretty well :)
                // Idea is for the resulting values to be comparable across different quantities of data samples
                sumSquares = Math.Sqrt(sumSquares / count);

                if (sumSquares < minSquares)
                {
                    // Best match found;
                    minSquares = sumSquares;
                    bestOffset = offset;
                }

                // Reset to start evaluating the next offset
                count = 0;
                sumSquares = 0;
            }

            return -bestOffset - biasSeconds;
        }

        /// <summary>
        /// Gets the total time (in seconds) between upper and lower.  Returns 0 if pause is null.
        /// </summary>
        /// <param name="pause"></param>
        /// <returns></returns>
        private static int GetBias(ValueRange<DateTime> pause)
        {
            if (pause != null)
            {
                return (int)(pause.Upper - pause.Lower).TotalSeconds;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// Export 2 tracks to csv for later comparison.
        /// </summary>
        /// <param name="activityTrack">Data track from activity</param>
        /// <param name="iBikeTrack">Data track from iBike</param>
        internal static void ExportTracks(INumericTimeDataSeries activityTrack, INumericTimeDataSeries iBikeTrack, string name)
        {
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(name);

                // Write Header
                writer.WriteLine("Start, " + activityTrack.StartTime.ToString() + ", " + iBikeTrack.StartTime.ToString() + ", " + (activityTrack.StartTime - iBikeTrack.StartTime).TotalSeconds);
                writer.WriteLine("Activity Seconds, Activity Value, iBike Seconds, iBike Value");

                for (int i = 0; i < Math.Max(activityTrack.Count, iBikeTrack.Count); i++)
                {
                    if (i >= activityTrack.Count)
                    {
                        // Only write track 2 data
                        writer.WriteLine(",," + iBikeTrack[i].ElapsedSeconds + ", " + iBikeTrack[i].Value);
                    }
                    else if (i >= iBikeTrack.Count)
                    {
                        // Only write track 1 data
                        writer.WriteLine(activityTrack[i].ElapsedSeconds + ", " + activityTrack[i].Value);
                    }
                    else
                    {
                        // Write both tracks
                        writer.WriteLine(activityTrack[i].ElapsedSeconds + ", " + activityTrack[i].Value + ", " + iBikeTrack[i].ElapsedSeconds + ", " + iBikeTrack[i].Value);
                    }
                }

                writer.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Finds the next pause in an activity after 'beginSearch' time.
        /// </summary>
        /// <param name="activity">Activity to search</param>
        /// <param name="beginSearch">Time in activity to begin searching for a pause.  
        /// Sections of the track before this will be ignored.</param>
        /// <returns>Returns next pause (in UTC I believe?)</returns>
        private static ValueRange<DateTime> GetNextPause(IActivity activity, DateTime beginSearch)
        {
            // Look through existing distance track finding pauses.  iBike automatically pauses/stops record below xx mph.
            IDistanceDataTrack distanceTrack = Utilities.GetDistanceTrack(activity);

            if (distanceTrack.Count == 0)
            {
                return null;
            }

            ITimeValueEntry<float> start = distanceTrack[0];
            ITimeValueEntry<float> end = distanceTrack[0];
            ITimeValueEntry<float> lastPoint = start;
            float speed;
            bool paused = false;
            uint stopsec = 0;

            // Find next iBike pause if it exists
            // TODO: Possible issues if you stop/restart timer while in motion?  Also applies to other routines.
            DateTime stop = distanceTrack.EntryDateTime(distanceTrack[distanceTrack.Count - 1]);
            foreach (ITimeValueEntry<float> point in distanceTrack)
            {
                // Don't look until after 'start'
                if (beginSearch.CompareTo(distanceTrack.EntryDateTime(point)) < 0)
                {
                    // Current instantaneous speed
                    speed = (point.Value - lastPoint.Value) / (point.ElapsedSeconds - lastPoint.ElapsedSeconds);

                    if (speed < thresholdSpeed)
                    {
                        // Start of pause?  Maybe...
                        stopsec += point.ElapsedSeconds - lastPoint.ElapsedSeconds;

                        // Pause Found.  iBike probably stopped recording.
                        if (stopsec > thresholdTime && !paused)
                        {
                            ushort seconds = (ushort)(point.ElapsedSeconds - stopsec + thresholdTime);
                            start = new TimeValueEntry<float>(seconds, point.Value);
                            paused = true;
                        }
                    }
                    else
                    {
                        // Check to see if we're in the middle of a pause
                        if (stopsec > thresholdTime)
                        {
                            // Complete pause found.  Add pause to track
                            end = point;

                            ValueRange<DateTime> pause = new ValueRange<DateTime>(distanceTrack.StartTime.AddSeconds(start.ElapsedSeconds), distanceTrack.StartTime.AddSeconds(end.ElapsedSeconds));
                            return pause;
                        }

                        // Reset pause detection
                        paused = false;
                        stopsec = 0;
                    }
                }

                lastPoint = point;
            }

            // No more pauses OR entire remainder of track is a pause (may need to handle this last scenario)
            return null;
        }

        #endregion

        #endregion

        #region Event Handlers


        #endregion
    }
}
