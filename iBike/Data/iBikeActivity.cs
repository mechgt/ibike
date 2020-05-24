using System;
using System.Collections.Generic;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.GPS;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Globalization;
using iBike.Resources;

namespace iBike.Data
{
    [Serializable()]  // Set this attribute to all the classes that want to serialize

    public class iBikeActivity : ISerializable
    {
        #region Fields

        private static SortedList<string, int> activityIndex = new SortedList<string, int>();

        private string refId;
        private string filename;
        private string notes;
        private int recordingInterval = 1;
        private IDistanceDataTrack distanceTrack = new DistanceDataTrack();
        private IGPSRoute gpsRoute = new GPSRoute();
        private INumericTimeDataSeries hrTrack = new NumericTimeDataSeries();
        private INumericTimeDataSeries windTrack = new NumericTimeDataSeries();
        private INumericTimeDataSeries tempCelsiusTrack = new NumericTimeDataSeries();
        private INumericTimeDataSeries tiltTrack = new NumericTimeDataSeries();
        private INumericTimeDataSeries cadenceTrack = new NumericTimeDataSeries();
        private INumericTimeDataSeries altitudeTrack = new NumericTimeDataSeries();
        private INumericTimeDataSeries powerTrack = new NumericTimeDataSeries();
        private INumericTimeDataSeries originalMatchTrack = new NumericTimeDataSeries();
        private TrackType matchType;
        private ValueRangeSeries<DateTime> timerPauses = new ValueRangeSeries<DateTime>();
        private float tempCelsius;
        private float energySpent;
        private float climbing;
        private float windScaling;
        private float atmPres;
        private DateTime mergeStartTime;
        private bool merge;
        private TimeSpan timeZoneOffset;
        private Dictionary<DateTime, TimeSpan> laps = new Dictionary<DateTime, TimeSpan>();

        #endregion

        #region Enumerations

        internal enum TrackType
        {
            Cadence,
            HeartRate,
            Elevation,
            Speed
        }

        #endregion

        #region Constructor

        internal iBikeActivity(IActivity activity)
        {
            // Reference ID is used by ReadData to id the activity
            if (activity != null)
            {
                refId = activity.ReferenceId;
            }
            else
            {
                refId = string.Empty;
            }

            ReadData();
        }

        internal iBikeActivity()
        {
            refId = string.Empty;
        }

        #endregion

        #region Properties

        public IActivity Activity
        {
            get
            {
                return GetActivity(refId);
            }
        }

        /// <summary>
        /// Determines if data is valid and OK to import.
        /// </summary>
        public bool ValidImport
        {
            get
            {
                // If any data tracks are populated, then it's ok for import
                if (!Utilities.IsNullOrEmpty(powerTrack) || !Utilities.IsNullOrEmpty(hrTrack) || !Utilities.IsNullOrEmpty(distanceTrack) || !Utilities.IsNullOrEmpty(cadenceTrack))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Distance from distance track, converted to user distance units (such as miles for instance)
        /// </summary>
        public float Distance
        {
            get
            {
                if (DistanceMetersTrack != null)
                {
                    float distance = DistanceMetersTrack.Max;
                    distance = (float)Length.Convert(distance, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits);
                    return distance;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Dictionary<DateTime, TimeSpan> Laps
        {
            get { return laps; }
        }

        public string ReferenceId
        {
            get { return refId; }
            set { refId = value; }
        }

        /// <summary>
        /// Gets or sets the iBike path and filename.
        /// This is the full path... C:\My Documents\iBike\file.csv
        /// </summary>
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public string Name
        { get; set; }

        /// <summary>
        /// Ride notes from iBike.
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public int RecordInterval
        {
            get { return recordingInterval; }
            set { recordingInterval = value; }
        }

        public float EnergySpent
        {
            get { return energySpent; }
            set { energySpent = value; }
        }

        public float TotalCalories
        {
            get
            {
                return energySpent * .95f;
            }
        }

        public float Climbing
        {
            get { return climbing; }
            set { climbing = value; }
        }

        public float AtmPres
        {
            get { return atmPres; }
            set { atmPres = value; }
        }

        public float WindScaling
        {
            get
            {
                if (float.IsNaN(windScaling))
                {
                    windScaling = 0;
                }

                return windScaling;
            }
            set { windScaling = value; }
        }

        /// <summary>
        /// Temperature in iBike (converted to Celsius)
        /// </summary>
        public float TemperatureCelsius
        {
            get
            {
                if (float.IsNaN(tempCelsius))
                {
                    tempCelsius = 0;
                }
                return tempCelsius;
            }
            set { tempCelsius = value; }
        }

        public IDistanceDataTrack DistanceMetersTrack
        {
            get { return distanceTrack; }
            set { distanceTrack = value; }
        }

        public IGPSRoute GPSRoute
        {
            get
            {
                if (gpsRoute == null || gpsRoute.TotalDistanceMeters < 15)
                {
                    return null;
                }

                return gpsRoute;
            }
            set { gpsRoute = value; }
        }

        public INumericTimeDataSeries HeartRateTrack
        {
            get
            {
                if (hrTrack == null || hrTrack.Avg == 0)
                {
                    return null;
                }

                return hrTrack;
            }
            set { hrTrack = value; }
        }

        public INumericTimeDataSeries CadenceTrack
        {
            get
            {
                if (cadenceTrack == null || cadenceTrack.Avg == 0)
                {
                    return null; ;
                }

                return cadenceTrack;
            }

            set { cadenceTrack = value; }
        }

        public INumericTimeDataSeries AltitudeTrackM
        {
            get
            {
                IActivity activity = Activity;

                if (Utilities.IsNullOrEmpty(altitudeTrack) && activity != null && !Utilities.IsNullOrEmpty(activity.ElevationMetersTrack))
                {
                    altitudeTrack = activity.ElevationMetersTrack;
                }

                return altitudeTrack;
            }
            set
            {
                altitudeTrack = value;
            }
        }

        public INumericTimeDataSeries PowerTrack
        {
            get { return powerTrack; }
            set { powerTrack = value; }
        }

        public INumericTimeDataSeries WindTrackKM
        {
            get { return windTrack; }
            set { windTrack = value; }
        }

        public INumericTimeDataSeries TempCelsuisTrack
        {
            get { return tempCelsiusTrack; }
            set { tempCelsiusTrack = value; }
        }

        public INumericTimeDataSeries TiltTrack
        {
            get { return tiltTrack; }
            set { tiltTrack = value; }
        }

        public INumericTimeDataSeries OriginalTrack
        {
            get
            {
                if (Utilities.IsNullOrEmpty(originalMatchTrack) && Activity != null)
                {
                    // Attempt to read in original track.
                    byte[] byteUserData = Activity.GetExtensionData(GUIDs.PluginMain);

                    if (byteUserData.Length > 0)
                    {
                        // Deserialize settings data from logbook
                        try
                        {
                            MemoryStream stream = new MemoryStream(byteUserData);
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Binder = new Binder(); // Help Deserializer resolve my class
                            stream.Position = 0;

                            // Deserialize data into temporary activity
                            iBikeActivity act = (iBikeActivity)formatter.Deserialize(stream);

                            // Store deserialized data to instance
                            // TODO: Could this ever be a stack overflow?
                            originalMatchTrack = act.OriginalTrack;
                        }
                        catch
                        { }
                    }
                }

                return originalMatchTrack;
            }
            set { originalMatchTrack = value; }
        }

        public ValueRangeSeries<DateTime> TimerPauses
        {
            get
            {
                return timerPauses;
            }
        }

        /// <summary>
        /// Gets a value indicating if this activity has been merged with another activity
        /// </summary>
        internal bool Merged
        {
            get { return merge; }
            set { merge = value; }
        }

        /// <summary>
        /// Gets or sets a value describing the type of track used for the original match track
        /// </summary>
        internal TrackType MatchTrackType
        {
            get { return matchType; }
            set { matchType = value; }
        }

        /// <summary>
        /// Gets track from activity based on MatchTrackType.  This is a reference to an activity track, not a stored track related to iBike.
        /// </summary>
        internal INumericTimeDataSeries MatchTrack
        {
            get
            {
                if (Activity == null)
                {
                    return null;
                }

                switch (MatchTrackType)
                {
                    case TrackType.Cadence:
                        return Activity.CadencePerMinuteTrack;
                    case TrackType.Elevation:
                        return Activity.ElevationMetersTrack;
                    case TrackType.HeartRate:
                        return Activity.HeartRatePerMinuteTrack;
                    case TrackType.Speed:
                        INumericTimeDataSeries speedTrack = Utilities.GetSpeedTrack(Activity.DistanceMetersTrack);

                        return speedTrack;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the start time for all of the data tracks.  This assumes that all data tracks start simultaneously.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                DateTime startTime = powerTrack.StartTime;

                if (startTime.CompareTo(hrTrack.StartTime) < 0 && hrTrack.StartTime != DateTime.MinValue)
                {
                    startTime = hrTrack.StartTime;
                }

                if (startTime.CompareTo(distanceTrack.StartTime) < 0 && distanceTrack.StartTime != DateTime.MinValue)
                {
                    startTime = distanceTrack.StartTime;
                }

                if (startTime.CompareTo(cadenceTrack.StartTime) < 0 && cadenceTrack.StartTime != DateTime.MinValue)
                {
                    startTime = cadenceTrack.StartTime;
                }

                if (startTime.CompareTo(altitudeTrack.StartTime) < 0 && altitudeTrack.StartTime != DateTime.MinValue)
                {
                    startTime = altitudeTrack.StartTime;
                }

                return startTime;
            }

            set
            {
                cadenceTrack = Utilities.SetTrackStart(cadenceTrack, value) as NumericTimeDataSeries;
                tiltTrack = Utilities.SetTrackStart(tiltTrack, value) as NumericTimeDataSeries;
                altitudeTrack = Utilities.SetTrackStart(altitudeTrack, value) as NumericTimeDataSeries;
                powerTrack = Utilities.SetTrackStart(powerTrack, value) as NumericTimeDataSeries;
                distanceTrack = Utilities.SetTrackStart(distanceTrack, value) as IDistanceDataTrack;
                hrTrack = Utilities.SetTrackStart(hrTrack, value) as NumericTimeDataSeries;
                windTrack = Utilities.SetTrackStart(windTrack, value) as NumericTimeDataSeries;
            }
        }

        /// <summary>
        /// Time zone offset from UTC time of the given activity at the time of the activity (considering DST, etc.).
        /// </summary>
        public TimeSpan TimeZoneOffset
        {
            get { return timeZoneOffset; }
            set { timeZoneOffset = value; }
        }

        /// <summary>
        /// Start time of activity for use when importing.  Used to set 'update activity' by default on import where appropriate.
        /// </summary>
        internal DateTime ImportStartTime
        {
            get
            {
                if (merge)
                {
                    return mergeStartTime;
                }
                else
                {
                    return StartTime;
                }
            }
            set
            {
                merge = true;
                mergeStartTime = value;
            }
        }

        #endregion

        # region Methods

        /// <summary>
        /// Adds a pause to activity AND inserts this pause into all of the data tracks.
        /// </summary>
        /// <param name="pause"></param>
        internal void AddPause(ValueRange<DateTime> pause)
        {
            // Add to list of pauses
            timerPauses.Add(pause);

            // Insert pause in data tracks
            cadenceTrack = Utilities.InsertPause(cadenceTrack, pause) as INumericTimeDataSeries;
            tiltTrack = Utilities.InsertPause(tiltTrack, pause) as INumericTimeDataSeries;
            altitudeTrack = Utilities.InsertPause(altitudeTrack, pause) as INumericTimeDataSeries;
            powerTrack = Utilities.InsertPause(powerTrack, pause) as INumericTimeDataSeries;
            distanceTrack = Utilities.InsertPause(distanceTrack, pause) as IDistanceDataTrack;
            hrTrack = Utilities.InsertPause(hrTrack, pause) as INumericTimeDataSeries;
            windTrack = Utilities.InsertPause(windTrack, pause) as INumericTimeDataSeries;

            // Update laps
            if (laps != null && laps.Count != 0)
            {
                Dictionary<DateTime, TimeSpan> updatedLaps = new Dictionary<DateTime, TimeSpan>();

                TimeSpan pauseDuration = pause.Upper - pause.Lower;
                Dictionary<DateTime, TimeSpan>.Enumerator lapEnum = laps.GetEnumerator();

                foreach (KeyValuePair<DateTime, TimeSpan> lap in laps)
                {
                    DateTime lapStart = lap.Key;
                    DateTime lapEnd = lap.Key.Add(lap.Value);

                    // Update end as necessary
                    if (pause.Lower < lapEnd)
                    {
                        lapEnd = lapEnd.Add(pauseDuration);
                    }

                    // Update start as necessary
                    if (pause.Lower < lapStart)
                    {
                        lapStart = lapStart.Add(pauseDuration);
                    }

                    // Store lap
                    updatedLaps.Add(lapStart, lapEnd - lapStart);
                }

                // Replace with new lap set
                laps = updatedLaps;
            }
        }

        /// <summary>
        /// Gets a list of the updated data tracks from an activity.  Used immediately after import 
        /// to get a list of iBike tracks that need to be syncd with the activities other tracks.
        /// </summary>
        /// <param name="activity">Activity to analyze</param>
        /// <returns>List of tracks that were imported from iBike.  This is a list of objects because of the DistanceMetersTrack.</returns>
        internal void ShiftUpdatedTracks(int seconds)
        {
            bool visible = DetailPage.iBikePane.Visible;
            DetailPage.iBikePane.Visible = false;

            IActivity activity = Activity;

            // Power Track
            INumericTimeDataSeries power = activity.PowerWattsTrack;
            activity.PowerWattsTrack = Utilities.ShiftNumericTracks(seconds, activity.PowerWattsTrack) as NumericTimeDataSeries;

            // Wind Track
            this.WindTrackKM = Utilities.ShiftNumericTracks(seconds, WindTrackKM) as NumericTimeDataSeries;

            // Temperature Track
            this.TempCelsuisTrack = Utilities.ShiftNumericTracks(seconds, TempCelsuisTrack) as NumericTimeDataSeries;

            // Tilt Track
            this.TiltTrack = Utilities.ShiftNumericTracks(seconds, TiltTrack) as NumericTimeDataSeries;

            // Heartrate Track
            if (IsIBikeTrack(activity.HeartRatePerMinuteTrack))
            {
                activity.HeartRatePerMinuteTrack = Utilities.ShiftNumericTracks(seconds, activity.HeartRatePerMinuteTrack) as NumericTimeDataSeries;
            }

            // Cadence Track
            if (IsIBikeTrack(activity.CadencePerMinuteTrack))
            {
                activity.CadencePerMinuteTrack = Utilities.ShiftNumericTracks(seconds, activity.CadencePerMinuteTrack) as NumericTimeDataSeries;
            }

            // Elevation Track
            if (IsIBikeTrack(activity.ElevationMetersTrack))
            {
                activity.ElevationMetersTrack = Utilities.ShiftNumericTracks(seconds, activity.ElevationMetersTrack) as NumericTimeDataSeries;
            }

            // Speed/Distance Track
            if (IsIBikeTrack(activity.DistanceMetersTrack))
            {
                activity.DistanceMetersTrack = Utilities.ShiftNumericTracks(seconds, activity.DistanceMetersTrack) as DistanceDataTrack;
            }

            DetailPage.iBikePane.Visible = visible;

            SaveData();
        }

        /// <summary>
        /// Parse and store activity header information.  These are things such as temperature, energy spent, etc.
        /// </summary>
        /// <param name="header">Header Line with items separated by a ','.</param>
        /// <returns>True if successful, or false if an error occurred.</returns>
        internal bool StoreHeaderData(string headerLine, bool metric, float version)
        {
            string[] data = Utilities.SplitCSVLine(headerLine);

            /* Data/Header Line Key:
             * [0] Weight (lb)
             * [1] energy spent (in kJ)
             * [2] Aero (from Profile)
             * [3] Friction
             * [4] Recording Interval (1 sec or 5 sec)
             * [5] Reference Elevation (ft)
             * [6] Climbing (feet)
             * [7] Wheel Circumference (mm)
             * [8] Ref. Temp. (°F)
             * [9] Ref. Atm Pres (mbar)
             * [10] ?? Internal
             * [11] Wind Scaling
             * [12] Riding Tilt (%)
             * [13] Total/Calibration Weight
             * [14] ?? Internal
             * [15] Cm (from profile... no idea what this is)
             * [16] CdA??? (Maybe what it was recorded with? Unsure, it disagrees on the ride I'm looking at 2/14/2010)
             * [17] Crr (questions as above probably apply)
             * [18] NOTE: Clear text (ver 13+)
             * [19] TimeZone offset (ver 13+)
             * [20] Creator (software description) (ver 13+)
             * [21] Ride Duration (time) (ver 13+)
             * [22] Ride Distance (mi) (ver 13+)
             * [23] Calories Burned (kcal) (ver 13+)
             * [24] Trainer Ride? (boolean) (ver 13+)
             * [25] Device description ("iBike Newton+" for example) (ver 13+)
             * [...]
             * [28] Encoded Notes (ver <= 12)
             */

            // Collect/parse header data
            // Error that will cause bad data
            float temp;
            if (!float.TryParse(data[4], NumberStyles.Number, CultureInfo.InvariantCulture, out temp))
            {
                throw new Exception(Strings.Text_CouldNotParseRecordInterval);
            }
            recordingInterval = (int)temp;

            // If these fail for any reason... who cares.
            float.TryParse(data[1], NumberStyles.Number, CultureInfo.InvariantCulture, out energySpent);
            if (float.TryParse(data[6], NumberStyles.Number, CultureInfo.InvariantCulture, out climbing) && !metric)
                climbing = (float)Length.Convert(climbing, Length.Units.Foot, Length.Units.Meter);
            if (float.TryParse(data[8], NumberStyles.Number, CultureInfo.InvariantCulture, out tempCelsius) && !metric)
                tempCelsius = (float)Temperature.Convert(tempCelsius, Temperature.Units.Fahrenheit, Temperature.Units.Celsius);
            float.TryParse(data[9], NumberStyles.Number, CultureInfo.InvariantCulture, out atmPres);
            float.TryParse(data[11], NumberStyles.Number, CultureInfo.InvariantCulture, out windScaling);


            // Store Notes
            if (version <= 12 && 28 < data.Length)
            {
                // Decode Notes
                notes = iBike.Importer.FileImporter.DecodeNotes(data[28]);
            }
            else if (13 <= version && 18 < data.Length)
            {
                // Notes in clear text.
                // TODO: (MED) Test notes when the contain a comma.  How is this stored/split out?
                notes = data[18];
            }

            // Store Timezone offset
            if (13 <= version)
            {
                float tzHours;
                float.TryParse(data[19], NumberStyles.Number, CultureInfo.InvariantCulture, out tzHours);
                TimeZoneOffset = new TimeSpan(0, (int)(tzHours * 60), 0);
            }
            else
            {
                // TODO: (HIGH) Test Offset time
                TimeZoneOffset = TimeZone.CurrentTimeZone.GetUtcOffset(StartTime);
            }

            return true;
        }

        /// <summary>
        /// Determines if this is an iBike track or not by comparing to the power track which is assumed to have been imported from the iBike.
        /// </summary>
        /// <param name="track">Track to check</param>
        /// <returns>Returns true if track is related to iBike, otherwise false</returns>
        private bool IsIBikeTrack(ITimeDataSeries<float> track)
        {
            if (Activity == null)
            {
                return false;
            }

            INumericTimeDataSeries power = Activity.PowerWattsTrack;

            if (track != null &&
                track.TotalElapsedSeconds == power.TotalElapsedSeconds &&
                track.Count == power.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Activity Index

        /// <summary>
        /// Get associated activity
        /// </summary>
        /// <param name="refId"></param>
        /// <returns></returns>
        private IActivity GetActivity(string refId)
        {
            if (string.IsNullOrEmpty(refId)) return null;

            ILogbook logbook = PluginMain.GetApplication().Logbook;

            // Try to find it in the index
            if (activityIndex.ContainsKey(refId))
            {
                try
                {
                    IActivity activity = logbook.Activities[activityIndex[refId]];
                    if (logbook.Activities[activityIndex[refId]].ReferenceId == refId)
                    {
                        return activity;
                    }
                    else
                    {
                        // Bad index found.  Remove (probably from removing or adding an activity)
                        activityIndex.Remove(refId);
                    }
                }
                catch
                { }
            }

            // Doesn't exist in the index, look through logbook (and build index)
            int i = 0;
            foreach (IActivity activity in logbook.Activities)
            {
                // Build index
                if (!activityIndex.ContainsKey(activity.ReferenceId))
                {
                    activityIndex.Add(activity.ReferenceId, i);
                }

                // Return activity
                if (activity.ReferenceId == refId)
                {
                    return activity;
                }

                i++;
            }

            return null;
        }

        #endregion

        #region ISerializable Members

        #region Serialize

        /// <summary>
        /// Serialize Data
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            string trackData;

            trackData = BuildTrackString(windTrack);
            info.AddValue("wind", trackData);
            info.AddValue("windStart", windTrack.StartTime);

            trackData = BuildTrackString(originalMatchTrack);
            info.AddValue("original", trackData);
            info.AddValue("originalStart", originalMatchTrack.StartTime);

            trackData = BuildTrackString(tiltTrack);
            info.AddValue("tilt", trackData);
            info.AddValue("tiltStart", tiltTrack.StartTime);

            info.AddValue("refId", refId);
            info.AddValue("filename", filename);
            info.AddValue("matchType", (int)matchType);

            info.AddValue("energySpent", energySpent);
            info.AddValue("recordInterval", recordingInterval);
            info.AddValue("climbing", climbing);
            info.AddValue("atmPres", atmPres);
            info.AddValue("windScaling", windScaling);
        }

        /// <summary>
        /// Build a string for serialization that represents the data track
        /// </summary>
        /// <param name="track">Track to get serialized string</param>
        /// <returns>String ready to serialize</returns>
        private static string BuildTrackString(INumericTimeDataSeries track)
        {
            StringBuilder builder = new StringBuilder();

            // Turn datatrack into long string
            foreach (TimeValueEntry<float> point in track)
            {
                builder.Append(point.ElapsedSeconds + "_" + point.Value.ToString() + "|");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Get Serialized data for this activity
        /// </summary>
        internal byte[] GetSerializedStream()
        {
            MemoryStream stream = new MemoryStream();

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
            catch
            { }

            // Return serialized bytes
            return stream.ToArray();
        }

        /// <summary>
        /// Save iBike data for this activity to logbook
        /// </summary>
        internal void SaveData()
        {
            if (refId != null)
            {
                byte[] data = GetSerializedStream();
                Activity.SetExtensionData(GUIDs.PluginMain, data);
            }
        }

        #endregion

        #region Deserialize

        /// <summary>
        /// Deserialize Data
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public iBikeActivity(SerializationInfo info, StreamingContext ctxt)
        {
            SerializationInfoEnumerator infoEnum = info.GetEnumerator();

            while (infoEnum.MoveNext())
            {
                string data;
                DateTime start;

                switch (infoEnum.Current.Name)
                {
                    case "refId":
                        refId = info.GetString("refId");
                        break;
                    case "matchType":
                        matchType = (TrackType)info.GetInt16("matchType");
                        break;
                    case "filename":
                        filename = info.GetString("filename");
                        break;
                    case "wind":
                        data = info.GetString("wind");
                        start = info.GetDateTime("windStart");
                        windTrack = new NumericTimeDataSeries(TranslateDataTrack(data, start));
                        break;
                    case "tilt":
                        data = info.GetString("tilt");
                        start = info.GetDateTime("tiltStart");
                        tiltTrack = new NumericTimeDataSeries(TranslateDataTrack(data, start));
                        break;
                    case "original":
                        data = info.GetString("original");
                        start = info.GetDateTime("originalStart");
                        originalMatchTrack = new NumericTimeDataSeries(TranslateDataTrack(data, start));
                        break;
                    case "energySpent":
                        energySpent = info.GetSingle(infoEnum.Current.Name);
                        break;
                    case "recordInterval":
                        recordingInterval = info.GetInt16(infoEnum.Current.Name);
                        break;
                    case "climbing":
                        climbing = info.GetSingle(infoEnum.Current.Name);
                        break;
                    case "atmPres":
                        atmPres = info.GetSingle(infoEnum.Current.Name);
                        break;
                    case "windScaling":
                        windScaling = info.GetSingle(infoEnum.Current.Name);
                        break;
                }
            }
        }

        private static NumericTimeDataSeries TranslateDataTrack(string data, DateTime start)
        {
            ushort sec;
            float value;
            NumericTimeDataSeries dataTrack = new NumericTimeDataSeries();

            string[] points = data.Split('|');

            // Store read in values
            foreach (string point in points)
            {
                string[] vals = point.Split('_');

                if (ushort.TryParse(vals[0], out sec) && float.TryParse(vals[1], out value))
                {
                    dataTrack.Add(start.AddSeconds(sec), value);
                }
            }

            return dataTrack;
        }

        /// <summary>
        /// Load data from logbook
        /// </summary>
        internal void ReadData()
        {
            ILogbook logbook = PluginMain.GetApplication().Logbook;

            if (logbook != null)
            {
                if (Activity == null) return;

                byte[] byteUserData = Activity.GetExtensionData(GUIDs.PluginMain);

                if (byteUserData.Length > 0)
                {
                    // Deserialize settings data from logbook
                    try
                    {
                        MemoryStream stream = new MemoryStream(byteUserData);
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Binder = new Binder(); // Help Deserializer resolve my class
                        stream.Position = 0;

                        // Deserialize data
                        iBikeActivity iBikeAct = (iBikeActivity)formatter.Deserialize(stream);

                        // Store deserialized data to instance
                        this.WindTrackKM = iBikeAct.WindTrackKM;
                        this.TempCelsuisTrack = iBikeAct.TempCelsuisTrack;
                        this.TiltTrack = iBikeAct.TiltTrack;
                        this.OriginalTrack = iBikeAct.OriginalTrack;
                        this.ReferenceId = iBikeAct.ReferenceId;
                        this.Filename = iBikeAct.Filename;
                        this.MatchTrackType = iBikeAct.MatchTrackType;
                        this.EnergySpent = iBikeAct.EnergySpent;
                        this.RecordInterval = iBikeAct.RecordInterval;
                        this.Climbing = iBikeAct.Climbing;
                        this.AtmPres = iBikeAct.AtmPres;
                        this.WindScaling = iBikeAct.WindScaling;
                    }
                    catch
                    { }
                }
            }
        }

        #endregion

        /// <summary>
        /// Serialization binder necessary for serializing (storing) data to logbook.
        /// </summary>
        private class Binder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type tyType = null;
                string shortName = assemblyName.Split(',')[0];
                System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                foreach (System.Reflection.Assembly assembly in assemblies)
                {
                    if (shortName == assembly.FullName.Split(',')[0])
                    {
                        tyType = assembly.GetType(typeName);
                        break;
                    }
                }

                return tyType;
            }
        }

        #endregion
    }
}
