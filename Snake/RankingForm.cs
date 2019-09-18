using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PlayerInfo;
namespace Snake
{
    public partial class RankingForm : Form
    {
        private List<Player> m_playerList = new List<Player>();        
        private XmlDocument m_rankingDoc = new XmlDocument();
        public RankingForm()
        {
            InitializeComponent();
           
            this.StartPosition = FormStartPosition.CenterParent;
            ReadRankingData();            
        }

        private void ReadRankingData()
        {
            m_rankingDoc.Load(Environment.CurrentDirectory.ToString() + "\\Ranking.xml");

            XmlElement root = m_rankingDoc.DocumentElement; 
            XmlNodeList playerNodeList = root.ChildNodes;

            foreach (XmlNode item in playerNodeList)
            {
                Player player = new Player();
                player.PlayerName = ((XmlElement)item).GetAttribute("name").Trim();
                player.GameLevel = Convert.ToInt32(((XmlElement)(item.ChildNodes[0])).InnerText.Trim());
                player.Score = Convert.ToInt32(((XmlElement)(item.ChildNodes[1])).InnerText.Trim());

                m_playerList.Add(player);
            }

            // 分数排序
            m_playerList.Sort();

            DataSet rankingDataSet = new DataSet();
            DataTable rankingDataTable = new DataTable("RankingTable");

            DataColumn rankCol = new DataColumn();
            rankCol.DataType = Type.GetType("System.Int32");
            rankCol.ColumnName = "排名";
            rankCol.ReadOnly = true;
            rankCol.Unique = true;
            rankingDataTable.Columns.Add(rankCol);

            DataColumn playerNameCol = new DataColumn();
            playerNameCol.DataType = Type.GetType("System.String");            
            playerNameCol.ColumnName = "玩家";
            playerNameCol.ReadOnly = true;
            playerNameCol.Unique = false;           
            rankingDataTable.Columns.Add(playerNameCol);

            DataColumn gameLevelCol = new DataColumn();
            gameLevelCol.DataType = Type.GetType("System.Int32");
            gameLevelCol.ColumnName = "游戏等级";
            gameLevelCol.ReadOnly = true;
            gameLevelCol.Unique = false;
            rankingDataTable.Columns.Add(gameLevelCol);

            DataColumn scoreCol = new DataColumn();
            scoreCol.DataType = Type.GetType("System.Int32");
            scoreCol.ColumnName = "游戏得分";
            scoreCol.ReadOnly = true;
            scoreCol.Unique = false;
            rankingDataTable.Columns.Add(scoreCol);
            
            for (int i = 0; i < m_playerList.Count; i++)
            {
                DataRow row = rankingDataTable.NewRow();
                row["排名"] = i + 1;
                row["玩家"] = m_playerList[i].PlayerName;
                row["游戏等级"] = m_playerList[i].GameLevel;
                row["游戏得分"] = m_playerList[i].Score;
                rankingDataTable.Rows.Add(row);
            }

            rankingDataSet.Tables.Add(rankingDataTable);
            this.dataGridViewRanking.DataSource = rankingDataSet;
            this.dataGridViewRanking.DataMember = "RankingTable";
        
            // 去掉表格排序功能            
            for (int i = 0; i < this.dataGridViewRanking.Columns.Count; i++)
            {
                this.dataGridViewRanking.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}