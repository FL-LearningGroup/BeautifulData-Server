using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime
{
    internal enum PiplelineScheduleApartTimeType
    {
        Y = 1, // Year.
        M, // Month.
        W, // Week.
        D, // Day
        HH, // Hour
        MM, //Minute
        SS, //Second
    }
    /*
    /// <summary>
    /// Pipeline execute schedule time
    /// </summary>
    [Serializable]
    internal class PiplelineScheduleTime
    {
        private PiplelineScheduleTimeType _model;
        private uint _year;
        private uint _month;
        private uint _week;
        private uint _day;
        private uint _hour;
        private uint _mintue;
        private uint _second;

        public PiplelineScheduleTime(PiplelineScheduleTimeType type)
        {
            _model = type;
        }
        public PiplelineScheduleTimeType Model { get { return _model; } }
        public uint this[uint index]
        {
            set
            {
                switch (index)
                {
                    case 0:
                        _year = value;
                        break;
                    case 1:
                        _month = value;
                        break;
                    case 2:
                        _week = value;
                        break;
                    case 3:
                        _day = value;
                        break;
                    case 4:
                        _hour = value;
                        break;
                    case 5:
                        _mintue = value;
                        break;
                    case 6:
                        _second = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Execute pipeline time index");
                }
            }
            get
            {
                switch (index)
                {
                    case 0: return _year;
                    case 1: return _month;
                    case 2: return _day;
                    case 3: return _hour;
                    case 4: return _mintue;
                    case 5: return _second;
                    default: 
                        throw new ArgumentOutOfRangeException("Execute pipeline time index");
                }
            }
        }
        public uint Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public uint Month
        {
            get { return _month; }
            set
            {
                _month = value;
            }
        }
        public uint Week
        {
            get { return _week; }
            set
            {
                _week = value;
            }
        }
        public uint Day
        {
            get { return _day; }
            set
            {
                _day = value;
            }
        }
        public uint Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
            }
        }
        public uint Minute
        {
            get { return _mintue; }
            set
            {
                _mintue = value;
            }
        }
        public uint Second
        {
            get { return _second; }
            set
            {
                _second = value;
            }
        }

    }
    */
    /// <summary>
    /// Pipeline execute schedule time
    /// </summary>
    [Serializable]
    internal class PiplelineScheduleTime
    {
        private string _startTime;
        private string _model;
        private int _apartTime;
        public string StartTime { get { return _startTime; } set { _startTime = value; } }
        public string Model { get { return _model; } set { _model = value; } }
        public int ApartTime { get { return _apartTime; } set { _apartTime = value; } }
    }
}
