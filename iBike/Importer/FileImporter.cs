using System.Globalization;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iBike.Data;
using iBike.DetailPage;
using iBike.Resources;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;

namespace iBike.Importer
{
    class FileImporter : IFileImporter
    {
        #region IFileImporter Members

        public string FileExtension
        {
            get
            {
                return "csv";
            }
        }

        public string Name
        {
            get
            {
                return Strings.Label_CSViBikeFile;
            }
        }

        public Guid Id
        {
            get
            {
                return GUIDs.FileImporter;
            }
        }

        public Image Image
        {
            get
            {
                // 24 pixel gif (png displays funny)
                return Images.iBikeWin24;
            }
        }

        #endregion

        #region IDataImporter Members

        public bool Import(string configurationInfo, IJobMonitor monitor, IImportResults importResults)
        {
            try
            {
                return iBikeImport(configurationInfo, monitor, importResults);
            }
            catch (Exception ex)
            {
                MessageDialog.Show(Strings.Text_ImportError + ":" + Environment.NewLine + ex.Message, Strings.Label_iBikeImport, MessageBoxButtons.OK);
                return false;
            }
        }

        #endregion

        #region Fields

        private static char delimeter = ',';

        #endregion

        #region Main iBike Import routines

        /// <summary>
        /// iBike specific import routine.  This is the main entry point into the file import.
        /// </summary>
        /// <param name="filename">iBike file to attempt import from</param>
        /// <param name="monitor">Link to send status messages</param>
        /// <param name="importResults">Import results</param>
        /// <returns></returns>
        internal static bool iBikeImport(string filename, IJobMonitor monitor, IImportResults importResults)
        {
            iBikeActivity importData = new iBikeActivity();

            if (!IsValidiBikeFile(filename))
            {
                // Not an iBike file
                return false;
            }

            // Read/parse the iBike file
            importData = ReadIBikeFile(filename);

            // Look for matching valid activities to update
            IActivity activity = FindActivityMatch(importData.StartTime);

            // Align Activity if necessary
            if (activity != null)
            {
                //SyncWithActivity(activity, ref importData);
                DialogResult result = MessageDialog.Show(Strings.Text_ActivityExistsWarning, Strings.Label_iBikeImport, MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }

            // Send final activity data to be imported
            if (importData.ValidImport)
            {
                ProcessImport(importData, importResults);
                ActivityCache.AddIBikeActivity(importData, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Read/Parse iBike CSV file into an activity container
        /// </summary>
        /// <param name="filename">iBike filename to read</param>
        /// <returns>Returns an activity container (custom class) with imported data.</returns>
        internal static iBikeActivity ReadIBikeFile(string fullpath)
        {
            iBikeActivity importData = new iBikeActivity();
            StreamReader file;
            float version;

            if (!IsValidiBikeFile(fullpath))
            {
                // Not an iBike file
                // An exception will be thrown rather than actually ever getting to this code.
                return null;
            }

            try
            {
                file = File.OpenText(fullpath);
            }
            catch
            {
                // This should be checked above (valid file), thus will never be hit.
                return null;
            }

            string line;
            string[] data;
            bool metric = false;

            importData.Filename = fullpath;
            GlobalSettings.Instance.Path = Path.GetDirectoryName(fullpath);

            /********************
             * Header Section
             *******************/
            // First line
            //1: "iBike,11,english
            line = file.ReadLine();
            data = line.Split(delimeter);

            if (3 <= data.Length)
            {
                // iBike format version
                float.TryParse(data[1], out version);

                // Units
                metric = data[2].ToLower() == "metric";
            }
            else
            {
                return null;
            }

            //2: 2009,11,15,09,59,38 - Note this is 'local' time
            line = file.ReadLine();
            DateTime iBikeStartTime = ParseDate(line);
            GlobalSettings.SetDateStyle(Path.GetFileName(fullpath), iBikeStartTime);

            // 3: Name line
            line = file.ReadLine();
            string name;

            if (13 <= version)
            {
                data = Utilities.SplitCSVLine(line);
                name = data[0];
            }
            else
            {
                name = line;
            }


            // 4: settings elements
            line = file.ReadLine();
            if (!importData.StoreHeaderData(line, metric, version))
            {
                // Header error encountered
                return null;
            }

            // 5: Header: Speed(mph), Wind Speed (mph),...
            // TODO: Does any of this change if we're not measuring in miles?
            file.ReadLine();

            /********************
             * Data Section
             *******************/

            // Import all data using iBike timestamps first.  
            // We'll adjust times and account for pauses, etc. later.

            float distance, cadence, hr, power, wind, tilt, elevation, DFPM, tempCelsius, lap, lat, lon;
            DateTime lapStart = DateTime.MinValue;

            NumericTimeDataSeries windTrack = new NumericTimeDataSeries();
            NumericTimeDataSeries tempCelsiusTrack = new NumericTimeDataSeries();
            NumericTimeDataSeries powerTrack = new NumericTimeDataSeries();
            DistanceDataTrack distanceData = new DistanceDataTrack();
            NumericTimeDataSeries cadenceTrack = new NumericTimeDataSeries();
            NumericTimeDataSeries heartRateData = new NumericTimeDataSeries();
            NumericTimeDataSeries altitudeTrack = new NumericTimeDataSeries();
            NumericTimeDataSeries tiltTrack = new NumericTimeDataSeries();
            IGPSRoute gpsRoute = new GPSRoute();

            // TODO: Issue can occur here if activity occurrec in timezone different from where it's being imported.
            // Regular tracks can be adjusted manually afterward, but wind & tilt tracks have no access
            // Timestamp entries in iBike are in local time and must be converted to UTC.
            //DateTime dataTime = iBikeStartTime.Add(importData.TimeZoneOffset);
            DateTime dataTime = iBikeStartTime.ToUniversalTime();

            bool usingDFPM = false;

            // Read and parse each line
            line = file.ReadLine();
            int count = 0;

            while (line != null)
            {
                data = line.Split(delimeter);
                count++;

                try
                {
                    // Collect all individual elements
                    wind = float.Parse(data[1], CultureInfo.InvariantCulture);
                    power = float.Parse(data[2], CultureInfo.InvariantCulture);
                    distance = float.Parse(data[3], CultureInfo.InvariantCulture);
                    cadence = float.Parse(data[4], CultureInfo.InvariantCulture);
                    hr = float.Parse(data[5], CultureInfo.InvariantCulture);
                    elevation = float.Parse(data[6], CultureInfo.InvariantCulture);
                    tilt = float.Parse(data[7], CultureInfo.InvariantCulture);
                    float.TryParse(data[9], NumberStyles.Integer, CultureInfo.InvariantCulture, out lap);
                    DFPM = float.Parse(data[11], CultureInfo.InvariantCulture);
                    tempCelsius = float.Parse(data[8], CultureInfo.InvariantCulture);
                    lat = float.Parse(data[12], CultureInfo.InvariantCulture);
                    lon = float.Parse(data[13], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(data[14]) && data[14].Length > 3)
                    {
                        // Use iBike 'Timestamp' when available.  Otherwise use header file.
                        dataTime = DateTime.Parse(data[14], CultureInfo.InvariantCulture).ToUniversalTime();
                    }

                    if (DFPM != 0 || usingDFPM)
                    {
                        // Import DFPM in place of iBike if available
                        usingDFPM = true;
                        power = DFPM;
                    }

                    // Convert values to metric if necessary
                    if (!metric)
                    {
                        wind = (float)Length.Convert(wind, Length.Units.Mile, Length.Units.Meter) / 3600;
                        elevation = (float)Length.Convert(elevation, Length.Units.Foot, Length.Units.Meter);
                        distance = (float)Length.Convert(distance, Length.Units.Mile, Length.Units.Meter);
                        tempCelsius = (float)Temperature.Convert(tempCelsius, Temperature.Units.Fahrenheit, Temperature.Units.Celsius);
                    }

                    // Store in data tracks (in metric units)
                    windTrack.Add(dataTime, wind);
                    tempCelsiusTrack.Add(dataTime, tempCelsius);
                    powerTrack.Add(dataTime, power);
                    distanceData.Add(dataTime, distance);
                    cadenceTrack.Add(dataTime, cadence);
                    heartRateData.Add(dataTime, hr);
                    altitudeTrack.Add(dataTime, elevation);
                    tiltTrack.Add(dataTime, tilt);
                    gpsRoute.Add(dataTime, new GPSPoint(lat, lon, elevation));
                    importData.TemperatureCelsius = ((count - 1) * importData.TemperatureCelsius + tempCelsius) / count;

                    if (lap == 1)
                    {
                        // Lap start found
                        if (lapStart != DateTime.MinValue)
                        {
                            // Add lap
                            DateTime lapEnd = dataTime;
                            importData.Laps.Add(lapStart, lapEnd - lapStart);
                        }

                        lapStart = dataTime;
                    }
                }
                catch
                {
                    // Error importing a particular line
                }

                // Next line of data
                line = file.ReadLine();

                // This time is calculated each iteration, however it's overwritten when available in timestamp column (value below is ignored)
                dataTime = dataTime.AddSeconds(importData.RecordInterval).ToUniversalTime();
            }

            // Complete laps as required
            if (lapStart != DateTime.MinValue)
            {
                // Add lap
                DateTime lapEnd = dataTime;
                importData.Laps.Add(lapStart, lapEnd - lapStart);
            }

            // Done reading file
            file.Close();

            importData.PowerTrack = powerTrack;
            importData.DistanceMetersTrack = distanceData;
            importData.AltitudeTrackM = altitudeTrack;
            importData.TiltTrack = tiltTrack;
            importData.WindTrackKM = windTrack;
            importData.TempCelsuisTrack = tempCelsiusTrack;

            // Only import if it contains valid data
            if (cadenceTrack.Avg > 15)
            {
                importData.CadenceTrack = cadenceTrack;
            }

            // Only import if it contains valid data
            if (heartRateData.Avg > 30)
            {
                importData.HeartRateTrack = heartRateData;
            }

            // Only import if it contains valid data
            if (gpsRoute.TotalDistanceMeters > 15)
            {
                importData.GPSRoute = gpsRoute;
            }

            // Activity Name
            importData.Name = name;

            return importData;
        }

        /// <summary>
        /// Processes data retrieved from import, and creates activity out of it to be sent to ST via importResults
        /// </summary>
        /// <param name="importData">Data collected from import.</param>
        /// <param name="importResults">Mechanism for sending data (activity) back to ST</param>
        internal static void ProcessImport(iBikeActivity iBikeAct, IImportResults importResults)
        {
            if (iBikeAct.ValidImport)
            {
                string version = typeof(FileImporter).Assembly.GetName().Version.ToString(2);

                IActivity activity = importResults.AddActivity(iBikeAct.ImportStartTime);
                activity.PowerWattsTrack = iBikeAct.PowerTrack;
                activity.DistanceMetersTrack = iBikeAct.DistanceMetersTrack;
                activity.CadencePerMinuteTrack = iBikeAct.CadenceTrack;
                activity.HeartRatePerMinuteTrack = iBikeAct.HeartRateTrack;
                activity.ElevationMetersTrack = iBikeAct.AltitudeTrackM;
                activity.GPSRoute = iBikeAct.GPSRoute;
                activity.Weather.TemperatureCelsius = iBikeAct.TemperatureCelsius;
                activity.TemperatureCelsiusTrack = iBikeAct.TempCelsuisTrack;
                activity.Metadata.Source = String.Format(Strings.Text_ActivityNote, version, Path.GetFileName(iBikeAct.Filename));
                activity.TotalAscendMetersEntered = iBikeAct.Climbing;
                activity.TotalDescendMetersEntered = -activity.TotalAscendMetersEntered;
                activity.TotalCalories = iBikeAct.TotalCalories;
                activity.Notes = DateTime.Now.ToLocalTime().ToShortDateString() + " - " + String.Format(Strings.Text_ActivityNote, new PluginMain().Version, Path.GetFileName(iBikeAct.Filename)) + Environment.NewLine + iBikeAct.Notes;
                activity.Name = iBikeAct.Name;

                foreach (ValueRange<DateTime> pause in iBikeAct.TimerPauses)
                {
                    activity.TimerPauses.Add(pause);
                }

                foreach (KeyValuePair<DateTime, TimeSpan> lap in iBikeAct.Laps)
                {
                    activity.Laps.Add(lap.Key, lap.Value);
                }

                // iBike custom data (wind track, etc.)
                byte[] data = iBikeAct.GetSerializedStream();
                activity.SetExtensionData(GUIDs.PluginMain, data);
            }
        }

        /// <summary>
        /// Finds a VALID activity with the closest matching start date (with a little user confirmation) or null if no activity found
        /// </summary>
        /// <param name="startTime">Start Time to match</param>
        /// <returns>Returns the matching activity or null if none found/accepted</returns>
        private static IActivity FindActivityMatch(DateTime startTime)
        {
            // All lines have been read and data tracks are created.
            // Find matching activity to align data with
            int bestMatchIndex = -1;
            TimeSpan error = TimeSpan.MaxValue;
            double startDifference = double.MaxValue;
            ILogbook logbook = iBike.PluginMain.GetApplication().Logbook;
            IActivity activity;
            List<string> ignoreList = new List<string>();

            while (true)
            {
                for (int index = 0; index < logbook.Activities.Count; index++)
                {
                    activity = logbook.Activities[index];

                    if ((activity.GPSRoute != null) && (activity.StartTime.Add(activity.TimeZoneUtcOffset).Date == startTime.Date))
                    {
                        // Find closest matching valid activity on same day
                        // Ignore activities user replied 'No' to.  This will find the next best match if another was found on this day.
                        if (Math.Abs((activity.StartTime - startTime).TotalSeconds) < startDifference && !ignoreList.Contains(activity.ReferenceId))
                        {
                            // Store closest valid matching activity
                            startDifference = Math.Abs((activity.StartTime - startTime).TotalSeconds);
                            bestMatchIndex = index;
                        }
                    }
                }

                if (bestMatchIndex == -1)
                {
                    // Continue to import as a standalone activity because no matches were found
                    break;
                }
                activity = logbook.Activities[bestMatchIndex];

                // Ask user to confirm activity found
                TimeSpan trackDuration = new TimeSpan(0, 0, (int)activity.GPSRoute.TotalElapsedSeconds);

                float distance = activity.TotalDistanceMetersEntered;
                Length.Units units = PluginMain.GetApplication().SystemPreferences.DistanceUnits;
                distance = (float)Math.Round(Length.Convert(distance, Length.Units.Meter, units), 1);

                //DialogResult res = MessageDialog.Show(string.Format(Resources.Images.ID_UseExistingGpsTrack, activity.StartTime.ToLocalTime(), distance, units, trackDuration.ToString()), Plugin.GetName(), MessageBoxButtons.YesNo);
                DialogResult res = DialogResult.Yes;

                if (res == DialogResult.Yes)
                {
                    // Continue and align to the selected activity
                    return activity;
                }
                else
                {
                    // Keep looking
                    ignoreList.Add(activity.ReferenceId);
                    bestMatchIndex = -1;
                    startDifference = double.MaxValue;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines if this is a valid iBike file or not.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Returns True if it's an iBike file, or throws an exception if it's not.</returns>
        private static bool IsValidiBikeFile(string filename)
        {
            bool valid = true;

            // Return false if file is locked
            while (IsFileLocked(new FileInfo(filename)))
            {
                DialogResult result = MessageDialog.Show(Strings.Text_FileLocked, Strings.Label_iBikeImport, MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    throw new Exception(string.Empty);
                }
            }

            StreamReader file = File.OpenText(filename);

            string line = file.ReadLine();
            if (line.IndexOf("iBike", 0) != 0)
            {
                // Valid iBike file
                throw new Exception(Strings.Text_InvalidFile);
            }

            line = file.ReadLine();
            if (ParseDate(line) == DateTime.MinValue)
            {
                throw new Exception(Strings.Text_CouldNotParseDate);
            }

            file.Close();
            return valid;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Parse date from CSV line
        /// </summary>
        /// <param name="line">CSV string that looks something like:
        /// "2009,11,15,09,59,38" --> Nov. 15th, 2009 at 9:59:38am
        /// </param>
        /// <returns></returns>
        private static DateTime ParseDate(string line)
        {
            try
            {
                string[] vals = line.Split(delimeter);

                DateTime date = new DateTime(
                    int.Parse(vals[0], CultureInfo.InvariantCulture),
                    int.Parse(vals[1], CultureInfo.InvariantCulture),
                    int.Parse(vals[2], CultureInfo.InvariantCulture),
                    int.Parse(vals[3], CultureInfo.InvariantCulture),
                    int.Parse(vals[4], CultureInfo.InvariantCulture),
                    int.Parse(vals[5], CultureInfo.InvariantCulture),
                    DateTimeKind.Local);

                return date;
            }
            catch
            {
                // Invalid time
                return DateTime.MinValue;
            }
        }

        internal static string DecodeNotes(string encoded)
        {
            encoded = encoded.Trim();
            string lower = encoded.Substring(0, encoded.Length / 2);
            string upper = encoded.Substring(encoded.Length / 2, encoded.Length / 2);

            string temp = string.Empty;
            int lowLen = lower.Length;
            int upLen = upper.Length;

            foreach (char c in lower)
            {
                temp += (c - 97).ToString("X");
            }
            lower = temp;

            temp = string.Empty;
            foreach (char c in upper)
            {
                temp += (c - 97).ToString("X"); ;
            }
            upper = temp;

            string[] lowPieces = lower.Split('-');
            string[] upPieces = upper.Split('-');

            string hex;
            string clearText = string.Empty;

            try
            {
                for (int i = 0; i < lower.Length; i++)
                {
                    hex = lower[i].ToString() + upper[i].ToString();
                    clearText += Convert.ToString(Convert.ToChar(Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber)));
                }

                clearText = clearText.Replace("\n", Environment.NewLine);

                return clearText;
            }
            catch (Exception ex)
            {
                return clearText + Environment.NewLine + ex.Message;
            }
        }

        /// <summary>
        /// Checks to see if a file is available to open
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        #endregion
    }
}
