using System.Globalization;
using System.Text;
using HtmlAgilityPack;
using ScheduleBot.Common.Dto;

namespace Parser
{
    public static class ScheduleParser
    {
        private static readonly int LENGTH_INVISIBLE_ElEMENTS = 3;
        private static readonly int LENGTH_TIME_VISITING = 11;

        public static List<ScheduleDto> GetShedule()
        {
            List<ScheduleDto> facilityInfo = new List<ScheduleDto>();
            var time = new List<List<string>>();

            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.UTF8;
            HtmlDocument document = web.Load("https://www.polessu.by/%D0%B3%D1%80%D0%B0%D1%84%D0%B8%D0%BA-%D0%BF%D0%BE%D1%81%D0%B5%D1%89%D0%B5%D0%BD%D0%B8%D1%8F-%D1%81%D0%BF%D0%BE%D1%80%D1%82%D0%B8%D0%B2%D0%BD%D1%8B%D1%85-%D0%BE%D0%B1%D1%8A%D0%B5%D0%BA%D1%82%D0%BE%D0%B2-%D1%81%D1%82%D1%83%D0%B4%D0%B5%D0%BD%D1%82%D0%B0%D0%BC%D0%B8-%D0%BF%D0%BE%D0%BB%D0%B5%D1%81%D0%B3%D1%83-%D0%BF%D0%BE-%D0%B4%D0%B8%D1%81%D1%86%D0%B8%D0%BF%D0%BB%D0%B8%D0%BD%D0%B5-%C2%AB%D1%84%D0%B8%D0%B7%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B0%D1%8F-%D0%BA%D1%83%D0%BB%D1%8C%D1%82%D1%83%D1%80%D0%B0%C2%BB");
            int count = 0;

            foreach (HtmlNode node in document.DocumentNode.SelectNodes("//table[contains(@class, 'MsoTableGrid')]//tr"))
            {
                if (count == 0)
                    count++;
                else
                {
                    var facilitySplit = node.InnerText.Split('\n');

                    for (int i = 1; i < facilitySplit.Length - 1; i++)
                    {
                        if (facilitySplit[i].Contains("\t"))
                        {
                            facilitySplit[i] = facilitySplit[i].Replace("\t\t\t", "");

                            if (new StringInfo(facilitySplit[i]).LengthInTextElements > LENGTH_INVISIBLE_ElEMENTS)
                                time[time.Count - 1].Add(facilitySplit[i]);
                            
                            facilitySplit[i] = string.Empty;
                        }
                        if (facilitySplit[i].Length > LENGTH_TIME_VISITING)
                        {
                            string str = string.Empty;

                            string timeOneDay = facilitySplit[i];

                            var timeVisiting = new List<string>();

                            for (int j = 0, strLength = 1; j < timeOneDay.Length; j++, strLength++)
                            {
                                str += timeOneDay[j];
                                if (strLength % LENGTH_TIME_VISITING == 0)
                                {
                                    timeVisiting.Add(str);
                                    str = string.Empty;
                                }
                            }
                            time.Add(timeVisiting);
                        }
                        else if (facilitySplit[i]!=string.Empty)
                            time.Add(new List<string> { facilitySplit[i] });
                    }
                    facilityInfo.Add(new ScheduleDto
                    {
                        NameFacility = facilitySplit[0],
                        Schedule = time
                    });
                    time = new List<List<string>>();
                }
            }
            return facilityInfo;
        }
    }
}

