using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;

namespace iBike.Controls
{
    public partial class SelectChartsForm : Form
    {
        public SelectChartsForm(List<DetailPaneChart.LineChartTypes> selectedCharts,
                                IActivity activity)
        {
            InitializeComponent();

            Graphics tempGraphics = this.CreateGraphics();

            this.Text = iBike.Properties.Resources.ChartDetails;

            MainPanel.ThemeChanged(Plugin.GetApplication().VisualTheme);
            MainPanel.BackColor = Plugin.GetApplication().VisualTheme.Control;

            AvailableChartLabel.Text = iBike.Properties.Resources.Available;
            SelectedChartLabel.Text = iBike.Properties.Resources.SelectedCharts;
            AvailableChartsList.Format += new ListControlConvertEventHandler(ChartsList_Format);
            DisplayedChartsList.Format += new ListControlConvertEventHandler(ChartsList_Format);
            AddChartButton.LeftImage = CommonResources.Images.DocumentAdd16;
            AddChartButton.Text = iBike.Properties.Resources.AddChart;
            MoveUpButton.LeftImage = CommonResources.Images.MoveUp16;
            MoveDownButton.LeftImage = CommonResources.Images.MoveDown16;
            RemoveChartButton.LeftImage = CommonResources.Images.Delete16;
            RemoveChartButton.Text = CommonResources.Text.ActionRemove;

            OKButton.Text = CommonResources.Text.ActionOk;
            Cancel_Button.Text = CommonResources.Text.ActionCancel;

            // Resize buttons for text, autosize doesn't work
            AddChartButton.Size = tempGraphics.MeasureString(AddChartButton.Text, AddChartButton.Font).ToSize() + new Size(32, 0); // Extra space for the image
            RemoveChartButton.Size = tempGraphics.MeasureString(RemoveChartButton.Text, RemoveChartButton.Font).ToSize() + new Size(32, 0); // Extra space for the image
            tempGraphics.Dispose();

            // Fill lists with current values
            m_SelectedCharts.Clear();
            m_SelectedCharts.AddRange(selectedCharts);

            m_Activity = activity;

            RefreshChartsList();
        }

        void ChartsList_Format(object sender, ListControlConvertEventArgs e)
        {
            DetailPaneChart.LineChartTypes item = (DetailPaneChart.LineChartTypes)e.ListItem;

            e.Value = DetailPaneChart.GetShortYAxisLabel(item, m_Activity);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AvailableChartsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableChartsList.SelectedItem != null)
            {
                DisplayedChartsList.SelectedItems.Clear();

                AddChartButton.Enabled = true;
                MoveUpButton.Enabled = false;
                MoveDownButton.Enabled = false;
                RemoveChartButton.Enabled = false;
            }
        }

        private void AvailableChartsList_DoubleClick(object sender, EventArgs e)
        {
            AddSelectedAvailableChart();
        }

        private void DisplayedChartsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DisplayedChartsList.SelectedItem != null)
            {
                AvailableChartsList.SelectedItems.Clear();

                AddChartButton.Enabled = false;
                MoveUpButton.Enabled = DisplayedChartsList.SelectedIndex != 0;
                MoveDownButton.Enabled = DisplayedChartsList.SelectedIndex != (DisplayedChartsList.Items.Count - 1);
                RemoveChartButton.Enabled = true;
            }
        }

        private void DisplayedChartsList_DoubleClick(object sender, EventArgs e)
        {
            RemoveSelectedDisplayedChart();
        }

        private void AddChartButton_Click(object sender, EventArgs e)
        {
            AddSelectedAvailableChart();
        }

        private void RemoveChartButton_Click(object sender, EventArgs e)
        {
            RemoveSelectedDisplayedChart();
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            if(DisplayedChartsList.SelectedItem != null)
            {
                int index = m_SelectedCharts.IndexOf((DetailPaneChart.LineChartTypes)DisplayedChartsList.SelectedItem) - 1;

                if (index >= 0)
                {
                    m_SelectedCharts.Reverse(index, 2);
                    RefreshChartsList();
                    DisplayedChartsList.SelectedIndex = index;
                }
            }
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            if (DisplayedChartsList.SelectedItem != null)
            {
                int index = m_SelectedCharts.IndexOf((DetailPaneChart.LineChartTypes)DisplayedChartsList.SelectedItem);

                if (index >= 0)
                {
                    m_SelectedCharts.Reverse(index, 2);
                    RefreshChartsList();
                    DisplayedChartsList.SelectedIndex = index + 1;
                }
            }
        }

        private void RefreshChartsList()
        {
            AvailableChartsList.Items.Clear();
            DisplayedChartsList.Items.Clear();

            for (int i = 0; i < (int)DetailPaneChart.LineChartTypes.Count; ++i)
            {
                if (!m_SelectedCharts.Contains((DetailPaneChart.LineChartTypes)i))
                {
                    AvailableChartsList.Items.Add((DetailPaneChart.LineChartTypes)i);
                }
            }

            foreach (DetailPaneChart.LineChartTypes displayedChart in m_SelectedCharts)
            {
                DisplayedChartsList.Items.Add(displayedChart);
            }
        }

        private void AddSelectedAvailableChart()
        {
            if (AvailableChartsList.SelectedItem != null)
            {
                DetailPaneChart.LineChartTypes chartToAdd = (DetailPaneChart.LineChartTypes)AvailableChartsList.SelectedItem;
                m_SelectedCharts.Add(chartToAdd);

                RefreshChartsList();

                DisplayedChartsList.SelectedItem = chartToAdd;
            }
        }

        private void RemoveSelectedDisplayedChart()
        {
            if (DisplayedChartsList.SelectedItem != null)
            {
                DetailPaneChart.LineChartTypes chartToRemove = (DetailPaneChart.LineChartTypes)DisplayedChartsList.SelectedItem;
                m_SelectedCharts.Remove(chartToRemove);

                RefreshChartsList();

                AvailableChartsList.SelectedItem = chartToRemove;
            }
        }

        public List<DetailPaneChart.LineChartTypes> SelectedCharts
        {
            get { return m_SelectedCharts; }
        }

        private List<DetailPaneChart.LineChartTypes> m_SelectedCharts = new List<DetailPaneChart.LineChartTypes>();
        private IActivity m_Activity;
    }
}