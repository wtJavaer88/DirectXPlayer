using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoDirectXPlayer.bean
{
    public class SrtInfo
    {
        protected int dbId;
        protected TimeInfo fromTime;
        protected TimeInfo toTime;
        protected int srtIndex;
        protected String chs;
        protected String eng;

        public TimeInfo getFromTime()
        {
            return fromTime;
        }

        public void setFromTime(TimeInfo fromTime)
        {
            this.fromTime = fromTime;
        }

        public TimeInfo getToTime()
        {
            return toTime;
        }

        public void setToTime(TimeInfo toTime)
        {
            this.toTime = toTime;
        }

        public int getSrtIndex()
        {
            return srtIndex;
        }

        public void setSrtIndex(int srtIndex)
        {
            this.srtIndex = srtIndex;
        }

        public String getChs()
        {
            return chs;
        }

        public void setChs(String chs)
        {
            this.chs = chs;
        }

        public String getEng()
        {
            return eng;
        }

        public void setEng(String eng)
        {
            this.eng = eng;
        }

        public int getDbId()
        {
            return dbId;
        }

        public void setDbId(int dbId)
        {
            this.dbId = dbId;
        }

        override
        public string ToString()
        {
            return "SrtInfo [dbId=" + dbId + ", fromTime=" + fromTime + ", toTime=" + toTime + ", srtIndex=" + srtIndex + ", chs=" + chs + ", eng=" + eng + "]";
        }

    }
}
