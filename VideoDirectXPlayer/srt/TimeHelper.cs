using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using basic;
using VideoDirectXPlayer.bean;

namespace VideoDirectXPlayer.srt
{
    public class TimeHelper
    {
        public static long getTime(TimeInfo timeInfo)
        {
            if (timeInfo == null)
            {
                return 0;
            }
            return 1000L * (3600 * timeInfo.getHour() + 60 * timeInfo.getMinute() + timeInfo.getSecond()) + timeInfo.getMillSecond();
        }

        public static TimeInfo parseTimeInfo(String timeStr)
        {
            List<string> patternStrings = new List<string>();
            Match m = Regex.Match(timeStr, @"\d+");
            while(m.Success){
                patternStrings.Add(m.Value);
                m=m.NextMatch();
            }
            int hour = BasicNumberUtil.getInt32(patternStrings[0]);
            int minute = BasicNumberUtil.getInt32(patternStrings[1]);
            int seconds = BasicNumberUtil.getInt32(patternStrings[2]);
            int millSecond = BasicNumberUtil.getInt32(patternStrings[3]);
            TimeInfo timeInfo = new TimeInfo();
            timeInfo.setHour(hour);
            timeInfo.setMinute(minute);
            timeInfo.setSecond(seconds);
            timeInfo.setMillSecond(millSecond);
            return timeInfo;
        }
    }
}
