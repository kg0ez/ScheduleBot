using System;
using System.Text;
using System.Xml;
using HtmlAgilityPack;

namespace Parser
{
    public static class ScheduleParser
    {
        public static List<List<string>> GetShedule()
        {
            List<string> days = new List<string>();
            List<List<string>> facilityInfo = new List<List<string>>();

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

                    facilityInfo.Add(new List<string> { facilitySplit[0] });

                    for (int i = 1; i < facilitySplit.Length - 1; i++)
                    {
                        if (facilitySplit[i].Length > 11)
                        {
                            string str = string.Empty;

                            string timeOneDay = facilitySplit[i];

                            var timeVisiting = new List<string>();

                            for (int j = 0, strLength = 1; j < timeOneDay.Length; j++, strLength++)
                            {
                                str += timeOneDay[j];
                                if (strLength % 11 == 0)
                                {
                                    timeVisiting.Add(str);
                                    str = string.Empty;
                                }
                            }
                            facilityInfo.Add(timeVisiting);
                        }
                        else
                            facilityInfo.Add(new List<string> { facilitySplit[i] });
                    }
                }
            }
            return facilityInfo;
        }
    }
}

