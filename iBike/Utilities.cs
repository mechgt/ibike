using ZoneFiveSoftware.Common.Visuals.Chart;
using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Drawing;

namespace iBike
{
    class Utilities
    {
        /// <summary>
        /// Get the distance track of an activity.  In order of priority, gets the actual distance track, or creates a track from GPS, or returns an empty track if no data exists.
        /// </summary>
        /// <param name="activity">Activity to get a distance track for</param>
        /// <returns>Distance track for an activity.  Returns an empty track if none can be found/calculated.</returns>
        public static IDistanceDataTrack GetDistanceTrack(IActivity activity)
        {
            IDistanceDataTrack distanceTrack;

            if (activity.DistanceMetersTrack != null)
            {
                // #1 Use Distance track from activity
                distanceTrack = activity.DistanceMetersTrack;
            }
            else
            {
                if (activity.GPSRoute != null)
                {
                    // #2 Otherwise create a distance track from GPS
                    distanceTrack = CreateDistanceDataTrack(activity);
                }
                else
                {
                    // Else, no distance track, and cannot create one.
                    distanceTrack = new DistanceDataTrack();
                }
            }

            return distanceTrack;
        }

        /// <summary>
        /// Gets the smoothed speed track for a particularly activity, in m/s
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static INumericTimeDataSeries GetSpeedTrack(IActivity activity)
        {
            if (activity == null) return null;

            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);

            INumericTimeDataSeries track = info.SmoothedSpeedTrack;
            if (Utilities.IsNullOrEmpty(track)) return null;

            // This method previously operated in mph, not m/s (RE: commented code below)
            //INumericTimeDataSeries speedTrack = new NumericTimeDataSeries();

            //foreach (TimeValueEntry<float> point in track)
            //{
            //    double value = Length.Convert(point.Value, Length.Units.Meter, activity.Category.DistanceUnits);
            //    value = value * 3600; // Convert from seconds to hour

            //    speedTrack.Add(track.StartTime.AddSeconds(point.ElapsedSeconds), (float)value);
            //}

            //return speedTrack;
            return track;
        }

        /// <summary>
        /// Get a smoothed speed track from a given distance track, in m/s.
        /// </summary>
        /// <param name="distanceTrack"></param>
        /// <returns></returns>
        public static INumericTimeDataSeries GetSpeedTrack(IDistanceDataTrack distanceTrack)
        {
            INumericTimeDataSeries speedTrack = new NumericTimeDataSeries();

            if (distanceTrack == null) return null;
            if (distanceTrack.Count == 0) return speedTrack;

            ITimeValueEntry<float> lastPoint = null;

            // Note that these default values will be the first entry in the speed track.
            float speed = 0;
            DateTime time = distanceTrack.StartTime;

            foreach (ITimeValueEntry<float> point in distanceTrack)
            {
                if (lastPoint != null)
                {
                    speed = (point.Value - lastPoint.Value) / (point.ElapsedSeconds - lastPoint.ElapsedSeconds);
                    //speed = (float)Length.Convert(speed, Length.Units.Meter, Length.Units.Mile);
                    //speed = speed * 3600; // Converts m/s to mph

                    time = distanceTrack.EntryDateTime(point);
                }

                speedTrack.Add(time, speed);
                lastPoint = point;
            }

            float min, max;
            speedTrack = STSmooth(speedTrack, PluginMain.GetApplication().SystemPreferences.AnalysisSettings.SpeedSmoothingSeconds, out min, out max);
            return speedTrack;
        }

        /// <summary>
        /// Convert a track encoded in UTC to a displayable data track with local timestamps.
        /// </summary>
        /// <param name="track"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static INumericTimeDataSeries ConvertToLocalTime(INumericTimeDataSeries track, TimeSpan timeZone)
        {
            NumericTimeDataSeries updated = new NumericTimeDataSeries();

            foreach (ITimeValueEntry<float> point in track)
            {
                updated.Add(track.EntryDateTime(point).Add(-timeZone), point.Value);
            }

            return updated;
        }

        /// <summary>
        /// Perform a smoothing operation using a moving average on the data series
        /// </summary>
        /// <param name="track">The data series to smooth</param>
        /// <param name="period">The range to smooth.  This is the total number of seconds to smooth across (slightly different than the ST method.)</param>
        /// <param name="min">An out parameter set to the minimum value of the smoothed data series</param>
        /// <param name="max">An out parameter set to the maximum value of the smoothed data series</param>
        /// <returns></returns>
        public static INumericTimeDataSeries Smooth(INumericTimeDataSeries track, int period, out float min, out float max)
        {
            min = float.NaN;
            max = float.NaN;
            INumericTimeDataSeries smooth = new NumericTimeDataSeries();

            if (!Utilities.IsNullOrEmpty(track) && period > 1)
            {
                //min = float.NaN;
                //max = float.NaN;
                int start = 0;
                int index = 0;
                float value = 0;
                float delta;

                float per = period;

                // Iterate through track
                // For each point, create average starting with 'start' index and go forward averaging 'period' seconds.
                // Stop when last 'full' period can be created ([start].ElapsedSeconds + 'period' seconds >= TotalElapsedSeconds)
                while (track[start].ElapsedSeconds + period < track.TotalElapsedSeconds)
                {
                    while (track[index].ElapsedSeconds < track[start].ElapsedSeconds + period)
                    {
                        delta = track[index + 1].ElapsedSeconds - track[index].ElapsedSeconds;
                        value += track[index].Value * delta;
                        index++;
                    }

                    // Finish value calculation
                    per = track[index].ElapsedSeconds - track[start].ElapsedSeconds;
                    value = value / per;

                    // Add value to track
                    // TODO: I really don't need the smoothed track... really just need max.  Kill this for efficiency?
                    //smooth.Add(track.StartTime.AddSeconds(start), value);
                    smooth.Add(track.EntryDateTime(track[index]), value);

                    // Remove beginning point for next cycle
                    delta = track[start + 1].ElapsedSeconds - track[start].ElapsedSeconds;
                    value = (per * value - delta * track[start].Value);

                    // Next point
                    start++;
                }

                max = smooth.Max;
                min = smooth.Min;
            }
            else if (!Utilities.IsNullOrEmpty(track) && period == 1)
            {
                min = track.Min;
                max = track.Max;
                return track;
            }

            return smooth;
        }

        /// <summary>
        /// SportTracks smoothing algorithm
        /// </summary>
        /// <param name="data"></param>
        /// <param name="seconds"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static INumericTimeDataSeries STSmooth(INumericTimeDataSeries data, int seconds, out float min, out float max)
        {
            min = float.NaN;
            max = float.NaN;
            if (Utilities.IsNullOrEmpty(data))
            {
                // Special case, no data
                return new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
            }
            else if (data.Count == 1 || seconds < 1)
            {
                // Special case
                INumericTimeDataSeries copyData = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
                min = data[0].Value;
                max = data[0].Value;
                foreach (ITimeValueEntry<float> entry in data)
                {
                    copyData.Add(data.StartTime.AddSeconds(entry.ElapsedSeconds), entry.Value);
                    min = Math.Min(min, entry.Value);
                    max = Math.Max(max, entry.Value);
                }
                return copyData;
            }
            min = float.MaxValue;
            max = float.MinValue;
            int smoothWidth = Math.Max(0, seconds * 2); // Total width/period.  'seconds' is the half-width... seconds on each side to smooth
            int denom = smoothWidth * 2; // Final value to divide by.  It's divide by 2 because we're double-adding everything
            INumericTimeDataSeries smoothedData = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();

            // Loop through entire dataset
            for (int nEntry = 0; nEntry < data.Count; nEntry++)
            {
                ITimeValueEntry<float> entry = data[nEntry];
                // TODO: Don't reset value & index markers, instead continue data here...
                double value = 0;
                double delta;
                // Data prior to entry
                long secondsRemaining = seconds;
                ITimeValueEntry<float> p1, p2;
                int increment = -1;
                int pos = nEntry - 1;
                p2 = data[nEntry];


                while (secondsRemaining > 0 && pos >= 0)
                {
                    p1 = data[pos];
                    if (SumValues(p2, p1, ref value, ref secondsRemaining))
                    {
                        pos += increment;
                        p2 = p1;
                    }
                    else
                    {
                        break;
                    }
                }
                if (secondsRemaining > 0)
                {
                    // Occurs at beginning of track when period extends before beginning of track.
                    delta = data[0].Value * secondsRemaining * 2;
                    value += delta;
                }
                // Data after entry
                secondsRemaining = seconds;
                increment = 1;
                pos = nEntry;
                p1 = data[nEntry];
                while (secondsRemaining > 0 && pos < data.Count - 1)
                {
                    p2 = data[pos + 1];
                    if (SumValues(p1, p2, ref value, ref secondsRemaining))
                    {
                        // Move to next point
                        pos += increment;
                        p1 = p2;
                    }
                    else
                    {
                        break;
                    }
                }
                if (secondsRemaining > 0)
                {
                    // Occurs at end of track when period extends past end of track
                    value += data[data.Count - 1].Value * secondsRemaining * 2;
                }
                float entryValue = (float)(value / denom);
                smoothedData.Add(data.StartTime.AddSeconds(entry.ElapsedSeconds), entryValue);
                min = Math.Min(min, entryValue);
                max = Math.Max(max, entryValue);

                // TODO: Remove 'first' p1 & p2 SumValues from 'value'
                if (data[nEntry].ElapsedSeconds - seconds < 0)
                {
                    // Remove 1 second worth of first data point (multiply by 2 because everything is double here)
                    value -= data[0].Value * 2;
                }
                else
                {
                    // Remove data in middle of track (typical scenario)
                    //value -= 
                }
            }
            return smoothedData;
        }

        /// <summary>
        /// Removes paused (but not stopped?) times in track.
        /// </summary>
        /// <param name="sourceTrack">Source data track to remove paused times</param>
        /// <param name="activity"></param>
        /// <returns>Returns an INumericTimeDataSeries with the paused times removed.</returns>
        public static INumericTimeDataSeries RemovePausedTimesInTrack(INumericTimeDataSeries sourceTrack, IActivity activity)
        {
            ActivityInfo activityInfo = ActivityInfoCache.Instance.GetInfo(activity);

            if (activityInfo != null && sourceTrack != null)
            {
                if (activityInfo.NonMovingTimes.Count == 0)
                {
                    return sourceTrack;
                }
                else
                {
                    INumericTimeDataSeries result = new NumericTimeDataSeries();
                    DateTime currentTime = sourceTrack.StartTime;
                    IEnumerator<ITimeValueEntry<float>> sourceEnumerator = sourceTrack.GetEnumerator();
                    IEnumerator<IValueRange<DateTime>> pauseEnumerator = activityInfo.NonMovingTimes.GetEnumerator();
                    double totalPausedTimeToDate = 0;
                    bool sourceEnumeratorIsValid;
                    bool pauseEnumeratorIsValid;

                    pauseEnumeratorIsValid = pauseEnumerator.MoveNext();
                    sourceEnumeratorIsValid = sourceEnumerator.MoveNext();

                    while (sourceEnumeratorIsValid)
                    {
                        bool addCurrentSourceEntry = true;
                        bool advanceCurrentSourceEntry = true;

                        // Loop to handle all pauses up to this current track point
                        if (pauseEnumeratorIsValid)
                        {
                            if (currentTime >= pauseEnumerator.Current.Lower &&
                                currentTime <= pauseEnumerator.Current.Upper)
                            {
                                addCurrentSourceEntry = false;
                            }
                            else if (currentTime > pauseEnumerator.Current.Upper)
                            {
                                // Advance pause enumerator
                                totalPausedTimeToDate += (pauseEnumerator.Current.Upper - pauseEnumerator.Current.Lower).TotalSeconds;
                                pauseEnumeratorIsValid = pauseEnumerator.MoveNext();

                                // Make sure we retry with the next pause
                                addCurrentSourceEntry = false;
                                advanceCurrentSourceEntry = false;
                            }
                        }

                        if (addCurrentSourceEntry)
                        {
                            result.Add(currentTime - new TimeSpan(0, 0, (int)totalPausedTimeToDate), sourceEnumerator.Current.Value);
                        }

                        if (advanceCurrentSourceEntry)
                        {
                            sourceEnumeratorIsValid = sourceEnumerator.MoveNext();
                            currentTime = sourceTrack.StartTime + new TimeSpan(0, 0, (int)sourceEnumerator.Current.ElapsedSeconds);
                        }
                    }

                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Shifts a track by a number of seconds.  Used to better align iBike tracks with activity.
        /// Very Similar to SetTrackStart().
        /// </summary>
        /// <param name="track"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static ITimeDataSeries<float> ShiftNumericTracks(int seconds, ITimeDataSeries<float> track)
        {
            DateTime start = track.StartTime;
            INumericTimeDataSeries newTrack = new NumericTimeDataSeries();

            foreach (TimeValueEntry<float> point in track)
            {
                newTrack.Add(start.AddSeconds(point.ElapsedSeconds + seconds), point.Value);
            }

            return newTrack;
        }

        /// <summary>
        /// Sets a new start time for a data track.  
        /// Very similar to ShiftNumericTracks().
        /// </summary>
        /// <param name="track"></param>
        /// <param name="start"></param>
        /// <returns>NumericTimeDataSeries with all data shifted to new start time.</returns>
        public static ITimeDataSeries<float> SetTrackStart(ITimeDataSeries<float> track, DateTime start)
        {
            IDistanceDataTrack newTrack = new DistanceDataTrack();

            foreach (TimeValueEntry<float> point in track)
            {
                newTrack.Add(start.AddSeconds(point.ElapsedSeconds), point.Value);
            }

            return newTrack;
        }

        /// <summary>
        /// It's absolutely necessary for these pauses to be inserted in the proper order!!! Insert earliest pause first, then go forward in time.
        /// </summary>
        /// <param name="track">Existing track to insert pause into</param>
        /// <param name="pause">Pause to be inserted</param>
        /// <returns></returns>
        public static ITimeDataSeries<float> InsertPause(ITimeDataSeries<float> track, ValueRange<DateTime> pause)
        {
            // DistanceDataTrack is (apparently) the most complex type, and the other two (TimeValueSeries and NumericTimeDataSeries) can be converted from this
            // Complex type -> (convert) -> Simple type : OK
            IDistanceDataTrack newTrack = new DistanceDataTrack();
            bool trackPauseFound = false;
            DateTime date;

            // Return track if it's invalid
            // Reasons: Empty or 'pause' is out of range
            if (Utilities.IsNullOrEmpty(track) ||
                pause.Upper < track.StartTime ||
                pause.Lower > track.EntryDateTime(track[track.Count - 1]))
            {
                return track;
            }

            // Search for insertion point
            foreach (TimeValueEntry<float> point in track)
            {
                if (track.EntryDateTime(point) >= pause.Lower && !trackPauseFound)
                {
                    date = pause.Upper;
                    trackPauseFound = true;
                }
                else if (trackPauseFound)
                {
                    date = track.EntryDateTime(point).Add(pause.Upper - pause.Lower);
                }
                else
                {
                    date = track.EntryDateTime(point);
                }

                // Add new points
                newTrack.Add(date, point.Value);
            }

            return newTrack;
        }

        /// <summary>
        /// Returns a value indicating if this track is null or empty.
        /// </summary>
        /// <param name="track"></param>
        public static bool IsNullOrEmpty(ITimeDataSeries<float> track)
        {
            if (track == null || track.Count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Temporary output routine.  Eventually should be deleted.
        /// </summary>
        /// <param name="track"></param>
        public static void ExportTrack(INumericTimeDataSeries track, string name)
        {
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(name);

                // Write Header
                writer.WriteLine("Seconds, Value, " + track.StartTime.ToLocalTime().ToString());

                foreach (ITimeValueEntry<float> item in track)
                {
                    // Write data
                    writer.WriteLine(item.ElapsedSeconds + ", " + item.Value);
                }

                writer.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Convert a data track that's time-based to a distance based data track.
        /// </summary>
        /// <returns></returns>
        public static SortedList<float, PointF> GetDistanceBasisPoints(INumericTimeDataSeries track, IActivity activity)
        {
            // TODO: Copied from Gears, BUT COMPLETELY UNTESTED

            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            IDistanceDataTrack distTrack = GetDistanceTrack(activity);
            float test = 0;
            SortedList<float, PointF> points = new SortedList<float, PointF>();

            foreach (ITimeValueEntry<float> entry in track)
            {
                DateTime time = distTrack.EntryDateTime(entry);
                float distance = distTrack.GetInterpolatedValue(time).Value;
                distance = (float)Length.Convert(distance, Length.Units.Meter, activity.Category.DistanceUnits);
                PointF point = new PointF(distance, entry.Value);
                
                if (!points.ContainsKey(point.X))
                {
                    points.Add(point.X, point);
                }
                else if (test != point.X)
                {
                    test = point.X;
                }
            }

            return points;
        }

        /// <summary>
        /// Used by the STSmooth Algorithm
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="value"></param>
        /// <param name="secondsRemaining"></param>
        /// <returns></returns>
        private static bool SumValues(ITimeValueEntry<float> p1, ITimeValueEntry<float> p2, ref double value, ref long secondsRemaining)
        {
            double spanSeconds = Math.Abs((double)p2.ElapsedSeconds - (double)p1.ElapsedSeconds);
            if (spanSeconds <= secondsRemaining)
            {
                value += (p1.Value + p2.Value) * spanSeconds;
                secondsRemaining -= (long)spanSeconds;
                return true;
            }
            else
            {
                double percent = (double)secondsRemaining / (double)spanSeconds;
                value += (p1.Value * ((float)2 - percent) + p2.Value * percent) * secondsRemaining;
                secondsRemaining = 0;
                return false;
            }
        }

        /// <summary>
        /// Create a distance track from an activity's GPS Route
        /// </summary>
        /// <param name="gpsActivity"></param>
        /// <returns>A distance track created from the GPS route</returns>
        private static IDistanceDataTrack CreateDistanceDataTrack(IActivity gpsActivity)
        {
            IDistanceDataTrack distanceTrack = new DistanceDataTrack();

            if (gpsActivity.GPSRoute != null && gpsActivity.GPSRoute.Count > 0)
            {
                float distance = 0;

                // First Point
                distanceTrack.Add(gpsActivity.GPSRoute.StartTime, 0);

                for (int i = 1; i < gpsActivity.GPSRoute.Count; i++)
                {
                    DateTime pointTime = gpsActivity.GPSRoute.StartTime.AddSeconds(gpsActivity.GPSRoute[i].ElapsedSeconds);
                    distance += gpsActivity.GPSRoute[i].Value.DistanceMetersToPoint(gpsActivity.GPSRoute[i - 1].Value);
                    distanceTrack.Add(pointTime, distance);
                }
            }

            return distanceTrack;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        internal static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return constructedString;
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        internal static byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        internal static string[] SplitCSVLine(string csvString)
        {
            string delimeter = ",";

            string[] fields;
            using (Microsoft.VisualBasic.FileIO.TextFieldParser parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(new System.IO.StringReader(csvString)))
            {
                parser.SetDelimiters(new string[] { delimeter.ToString() });
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(delimeter);

                fields = parser.ReadFields();

                parser.Close();
            }

            return fields;
        }
    }
}
