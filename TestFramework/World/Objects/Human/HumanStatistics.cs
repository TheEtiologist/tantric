using System;
using System.Collections.Generic;
using System.Text;

namespace Tantric_Testbed.World.Objects.Human
{
    public static class HumanStatistics
    {
        private static Dictionary<String, StatisticSet> m_Statistics = new Dictionary<string,StatisticSet>();
        public static void LoadStatistics(String file)
        {
            // Load up the file and define statistics for human units here
            // For now, just create a few test
            UpdateStatistic("Human_Assembler", "Speed", 300);
            UpdateStatistic("Camera", "Speed", 5);
        }

        public static void UpdateStatistic(String unit, String Stat, int value)
        {
            if (!m_Statistics.ContainsKey(unit))
                m_Statistics[unit] = new StatisticSet();
            m_Statistics[unit].UpdateStatistic(Stat, value);
        }

        public static int GetStatistic(String unit, String Statistic)
        {
            if (m_Statistics.ContainsKey(unit))
                return m_Statistics[unit].GetStatistic(Statistic);
            return 0;
        }
    }

    class StatisticSet
    {
        private Dictionary<String, int> m_Statistics = new Dictionary<string,int>();

        public void UpdateStatistic(String stat, int Value)
        {
            m_Statistics[stat] = Value;
        }

        public int GetStatistic(String statistic)
        {
            if (m_Statistics.ContainsKey(statistic))
                return m_Statistics[statistic];
            return 0;
        }
    }
}
