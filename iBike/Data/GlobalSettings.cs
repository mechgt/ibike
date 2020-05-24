using System.Xml.Serialization;
using System;
using System.Globalization;

namespace iBike.Data
{
    [XmlRootAttribute(ElementName = "iBike", IsNullable = false)]
    public class GlobalSettings
    {
        private static bool importCadence;
        private static bool importCalories;
        private static bool importDistance;
        private static bool importElevation;
        private static bool importHR;
        private static bool importPower;
        private static bool importTilt;
        private static bool importWind;
        private static bool importTemp;
        private static bool importGPS;
        private static bool importName;
        private static int dateIndex;
        private static DateFormat dateStyle;
        private static string path;
        private static GlobalSettings settings;

        public enum DateFormat
        {
            YYYY_MM_DD,
            MM_DD_YYYY,
            DD_MM_YYYY
        }

        public static GlobalSettings Instance
        {
            get
            {
                if (settings == null)
                {
                    settings = new GlobalSettings();
                }

                return settings;
            }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public DateFormat DateStyle
        {
            get { return dateStyle; }
            set { dateStyle = value; }
        }

        public int DateIndex
        {
            get { return dateIndex; }
            set { dateIndex = value; }
        }

        public bool Cadence
        {
            get { return importCadence; }
            set { importCadence = value; }
        }

        public bool Calories
        {
            get { return importCalories; }
            set { importCalories = value; }
        }

        public bool Distance
        {
            get { return importDistance; }
            set { importDistance = value; }
        }

        public bool Elevation
        {
            get { return importElevation; }
            set { importElevation = value; }
        }

        public bool GPS
        {
            get { return importGPS; }
            set { importGPS = value; }
        }

        public bool HeartRate
        {
            get { return importHR; }
            set { importHR = value; }
        }

        public bool Name
        {
            get { return importName; }
            set { importName = value; }
        }

        public bool Power
        {
            get { return importPower; }
            set { importPower = value; }
        }

        public bool Temperature
        {
            get { return importTemp; }
            set { importTemp = value; }
        }

        public bool Tilt
        {
            get { return importTilt; }
            set { importTilt = value; }
        }

        public bool Wind
        {
            get { return importWind; }
            set { importWind = value; }
        }

        public static bool SetDateStyle(string filename, DateTime date)
        {
            string[] parts = filename.Split('_');

            for (int i = 0; i + 2 < parts.Length; i++)
            {
                if (parts[i] == date.Year.ToString("0000", CultureInfo.InvariantCulture) && parts[i + 1] == date.Month.ToString("00", CultureInfo.InvariantCulture) && parts[i + 2] == date.Day.ToString("00", CultureInfo.InvariantCulture))
                {
                    GlobalSettings.Instance.DateStyle = DateFormat.YYYY_MM_DD;
                    GlobalSettings.Instance.DateIndex = i;
                    return true;
                }
                else if (parts[i] == date.Day.ToString("00", CultureInfo.InvariantCulture) && parts[i + 1] == date.Month.ToString("00", CultureInfo.InvariantCulture) && parts[i + 2] == date.Year.ToString("0000", CultureInfo.InvariantCulture))
                {
                    GlobalSettings.Instance.DateStyle = DateFormat.DD_MM_YYYY;
                    GlobalSettings.Instance.DateIndex = i;
                    return true;
                }
                else if (parts[i] == date.Month.ToString("00", CultureInfo.InvariantCulture) && parts[i + 1] == date.Day.ToString("00", CultureInfo.InvariantCulture) && parts[i + 2] == date.Year.ToString("0000", CultureInfo.InvariantCulture))
                {
                    GlobalSettings.Instance.DateStyle = DateFormat.MM_DD_YYYY;
                    GlobalSettings.Instance.DateIndex = i;
                    return true;
                }
            }

            return false;
        }
    }
}
