using MVC.Controller;
using MVC.DAL;
using MVC.Domain;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using MVC.DAL.CyberSport;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

namespace MVC.View
{
    public partial class MainForm : Form
    {
        MainController _maincontroller = new MainController();
        int I;
        
        public MainForm()
        {
            InitializeComponent();
            _maincontroller.InitParser();

            IParserSettings settings2 = new CsPasrseSettings(1, 3, "future");
            _maincontroller.settingsParser(settings2, "future");
            _maincontroller.StartParser();
        }

        private void ShowInfo(Competition info)
        {
            if (info == null) return;
            NameTournamentLabel.Text = info.TournamentName;
            NameOfGameLabel.Text = "Игра: " + info.NameOfGame.ToUpper();
            FirstCommand.Text = info.Commands[0].Trim();
            SecondCommand.Text = info.Commands[1].Trim();
            _maincontroller.url = _maincontroller.GetBaseUrl + info.url;
            DateTimeLabel.Text = info.Date;

            ListTeams.DataSource = info.Commands;
            EventsOfGameList.DataSource = info.EventsTournament;

            ChangeEventsList.DataSource = info.EventsTournament;
            ChangeEventText.Text = ChangeEventsList.SelectedItem as string;

            if (info.EventsTournament.Count == 0) TextOfEvent.Text = "Нет событий";
            else TextOfEvent.Text = info.EventsTournament[0];
        }

        

        private void ВыбратьДеньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*var Tournaments = _maincontroller.GetCompetitions();
            if (Tournaments.Count == 0) { NameTournamentLabel.Text = "На сегодня игр нет!"; return;  }
            ListCompetitions1.DataSource = Tournaments;
            ShowInfo(ListCompetitions1.SelectedItem as Competition);*/
        }

        private void ListFilms_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _compatition = ListCompetitions1.SelectedItem as Competition;
            ShowInfo(_compatition);
            I = 0;
        }

        private void OpenBrowseButton_Click(object sender, EventArgs e)
        {
            Process.Start(_maincontroller.url);
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void показатьБудущиеИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IParserSettings settings = new CsPasrseSettings(_maincontroller.StartPoint, _maincontroller.EndPoint, "future");
            _maincontroller.settingsParser(settings, "future");
            _maincontroller.StartParser();
        }

        private void ListCompetitions2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _compatition = ListCompetitions2.SelectedItem as Competition;
            ShowInfo(_compatition);
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void загрузитьДанныеССайтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IParserSettings settings = new CsPasrseSettings(1, 2, "active");
            _maincontroller.settingsParser(settings, "today");
            _maincontroller.StartParser();

            

        }

        private void показатьАктуальныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_maincontroller.LocCompetitions.Count != 0) _maincontroller.LocCompetitions = ListCompetitions1.DataSource as List<Competition>;
            var Tournaments = _maincontroller.GetCompetitions;
            if (Tournaments.Count == 0) { NameTournamentLabel.Text = "На сегодня игр нет!"; return; }
            ListCompetitions1.DataSource = Tournaments;
            ShowInfo(ListCompetitions1.SelectedItem as Competition);

            var Tournaments2 = _maincontroller.GetFutureCompetitions();
            if (Tournaments2.Count == 0) { NameTournamentLabel.Text = "Игр нет!"; return; }
            ListCompetitions2.DataSource = Tournaments2;
            ShowInfo(ListCompetitions2.SelectedItem as Competition);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string EventOfGame;
            if (NameOfGamersLabel.Text == "")
            {
                EventOfGame = ListTeams.SelectedItem.ToString() + " " + EventOfGameText.Text;
            }
            else
            {
                EventOfGame = NameOfGamersLabel.Text + $" из {ListTeams.SelectedItem.ToString()} {EventOfGameText.Text}";
            }

            var _competition = ListCompetitions1.SelectedItem as Competition;
            _competition.EventsTournament.Add(EventOfGame);

            MessageBox.Show(EventOfGame);
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            var competition = ListCompetitions1.SelectedItem as Competition;
            int left = 0;
            int right = competition.EventsTournament.Count - 1;

            I++;
            if (competition.EventsTournament.Count == 0) return;
            if (I > right) { I = 0; }
            TextOfEvent.Text = competition.EventsTournament[I];
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            var competition = ListCompetitions1.SelectedItem as Competition;
            int left = 0;
            int right = competition.EventsTournament.Count - 1;

            I--;
            if (competition.EventsTournament.Count == 0) return;
            if (I < left) { I = right; }
            TextOfEvent.Text = competition.EventsTournament[I];
        }

        private void выгрузитьИнформациюВФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\Users\tawer\OneDrive\Рабочий стол\123.xlsx";
            List<string> chars = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", };

            /*
            * 
            * Код для работы с экселем
            * 
            * Запись в эксель файл
            */


            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWb = xlApp.Workbooks.Open(fileName);
            Excel.Worksheet xlSht = xlWb.Sheets[1];
           
            int iLastRow = 1;
            var competitions = ListCompetitions1.DataSource as List<Competition>;
            int N = competitions.Count;

            xlSht.Cells[1, "A"].Value = "Название турнира";
            xlSht.Cells[1, "B"].Value = "Игра";
            xlSht.Cells[1, "C"].Value = "Первая команда";
            xlSht.Cells[1, "D"].Value = "Вторая команда";
            xlSht.Cells[1, "E"].Value = "Дата игры";
            xlSht.Cells[1, "F"].Value = "События игры";

            int k = 0;

            for (int i = 2; i < N + 2; i++)
            {
                xlSht.Cells[i, "A"].Value = competitions[k].TournamentName;
                xlSht.Cells[i, "B"].Value = competitions[k].NameOfGame;
                xlSht.Cells[i, "C"].Value = competitions[k].Commands[0];
                xlSht.Cells[i, "D"].Value = competitions[k].Commands[1];
                xlSht.Cells[i, "E"].Value = competitions[k].Date;
                string eventsOfGame = " ";

                for (int j = 0; j < competitions[k].EventsTournament.Count; j++)
                {
                    eventsOfGame += competitions[k].EventsTournament[j] + "\n";
                }
                xlSht.Cells[i, "F"].Value = eventsOfGame;

                k++;
            }
            xlWb.Close(true); 
            xlApp.Quit();
            MessageBox.Show("Файл успешно сохранён!");
            /*

                
            */
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void загрузитьИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListCompetitions2.DataSource = null;
            if (_maincontroller.GetCompetitions != null) _maincontroller.GetCompetitions = ListCompetitions1.DataSource as List<Competition>;
            if (_maincontroller.LocCompetitions.Count != 0)
            {
                ListCompetitions1.DataSource = _maincontroller.LocCompetitions;
                return;
            }

            string fileName = @"C:\Users\tawer\OneDrive\Рабочий стол\main.xlsx";
            List<string> chars = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", };

            /*
            * 
            * Код для работы с экселем
            * 
            * Вывод информации из эксель файла
            */
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWb = xlApp.Workbooks.Open(fileName);
            Excel.Worksheet xlSht = xlWb.Sheets[1];

            int iLastRow = xlSht.Cells[xlSht.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;
            var arrData = (object[,])xlSht.Range["A2:F" + iLastRow].Value;
            xlWb.Close(false); //закрыть и сохранить книгу
            xlApp.Quit();
            GC.Collect();

            int i, j;
            int RowsCount = arrData.GetUpperBound(0);
            int ColumnsCount = arrData.GetUpperBound(1);
            var competitions = new List<Competition>();
            for (i = 1; i <= RowsCount; i++)
            {
                var competition = new Competition();
                competition.TournamentName = arrData[i, 1].ToString();
                competition.NameOfGame = arrData[i, 2].ToString();
                competition.Commands = new[] { arrData[i, 3].ToString(), arrData[i, 4].ToString() };
                competition.Date = arrData[i, 5].ToString();
                string temp = arrData[i, 6].ToString();
                string buf = "";
                for (int k = 0; k < temp.Length; k++)
                {
                    if (temp[k] == '\n')
                    {
                        competition.EventsTournament.Add(buf.Trim());
                        buf = "";
                    }
                    buf += temp[k];
                }
                competitions.Add(competition);
            }

            _maincontroller.LocCompetitions = competitions;
            ListCompetitions1.DataSource = competitions;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newEvent = ChangeEventText.Text;
            List<Competition> competitions = ListCompetitions1.DataSource as List<Competition>;
            competitions[ListCompetitions1.SelectedIndex].EventsTournament[ChangeEventsList.SelectedIndex] = newEvent;
            ListCompetitions1.DataSource = competitions;
            MessageBox.Show("Изменения сохранены");

        }

        private void ChangeEventsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeEventText.Text = ChangeEventsList.SelectedItem as string;
        }
    }
}
