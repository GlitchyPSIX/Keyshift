
using Keyshift.Forms.Properties;

namespace Keyshift.Forms.Controls
{
    partial class TimelineRenderer
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.scControls = new System.Windows.Forms.SplitContainer();
            this.scTimelineSplit = new System.Windows.Forms.SplitContainer();
            this.scRackTitles = new Keyshift.Forms.Controls.ScrollableSplitContainer();
            this.btnTimelineOptions = new System.Windows.Forms.Button();
            this.lbTimecode = new System.Windows.Forms.Label();
            this.lbRackTitles = new System.Windows.Forms.ListBox();
            this.scRacksTrackhead = new System.Windows.Forms.SplitContainer();
            this.pnlRacks = new System.Windows.Forms.Panel();
            this.cbSync = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnFrontKf = new System.Windows.Forms.Button();
            this.btnAdvance = new System.Windows.Forms.Button();
            this.btnBackKf = new System.Windows.Forms.Button();
            this.btnStepback = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnNudgeR = new System.Windows.Forms.Button();
            this.btnNudgeL = new System.Windows.Forms.Button();
            this.btnRemoveKf = new System.Windows.Forms.Button();
            this.btnAddKf = new System.Windows.Forms.Button();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.ttButtonInfo = new System.Windows.Forms.ToolTip(this.components);
            this.cmsKeyframeSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmLinear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFast = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSlow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSmooth = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSharp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHold = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmBezier = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsWhy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.whyCantIControlTheTimelineWithTheMouseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scControls)).BeginInit();
            this.scControls.Panel1.SuspendLayout();
            this.scControls.Panel2.SuspendLayout();
            this.scControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTimelineSplit)).BeginInit();
            this.scTimelineSplit.Panel1.SuspendLayout();
            this.scTimelineSplit.Panel2.SuspendLayout();
            this.scTimelineSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scRackTitles)).BeginInit();
            this.scRackTitles.Panel1.SuspendLayout();
            this.scRackTitles.Panel2.SuspendLayout();
            this.scRackTitles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scRacksTrackhead)).BeginInit();
            this.scRacksTrackhead.Panel2.SuspendLayout();
            this.scRacksTrackhead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            this.cmsKeyframeSettings.SuspendLayout();
            this.cmsWhy.SuspendLayout();
            this.SuspendLayout();
            // 
            // scControls
            // 
            this.scControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scControls.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scControls.Location = new System.Drawing.Point(0, 0);
            this.scControls.Margin = new System.Windows.Forms.Padding(0);
            this.scControls.Name = "scControls";
            this.scControls.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scControls.Panel1
            // 
            this.scControls.Panel1.AutoScroll = true;
            this.scControls.Panel1.Controls.Add(this.scTimelineSplit);
            // 
            // scControls.Panel2
            // 
            this.scControls.Panel2.Controls.Add(this.cbSync);
            this.scControls.Panel2.Controls.Add(this.label2);
            this.scControls.Panel2.Controls.Add(this.nudLength);
            this.scControls.Panel2.Controls.Add(this.label1);
            this.scControls.Panel2.Controls.Add(this.btnEnd);
            this.scControls.Panel2.Controls.Add(this.btnPlay);
            this.scControls.Panel2.Controls.Add(this.btnFrontKf);
            this.scControls.Panel2.Controls.Add(this.btnAdvance);
            this.scControls.Panel2.Controls.Add(this.btnBackKf);
            this.scControls.Panel2.Controls.Add(this.btnStepback);
            this.scControls.Panel2.Controls.Add(this.btnStop);
            this.scControls.Panel2.Controls.Add(this.btnStart);
            this.scControls.Panel2.Controls.Add(this.btnNudgeR);
            this.scControls.Panel2.Controls.Add(this.btnNudgeL);
            this.scControls.Panel2.Controls.Add(this.btnRemoveKf);
            this.scControls.Panel2.Controls.Add(this.btnAddKf);
            this.scControls.Panel2.Controls.Add(this.tbZoom);
            this.scControls.Panel2MinSize = 36;
            this.scControls.Size = new System.Drawing.Size(971, 523);
            this.scControls.SplitterDistance = 486;
            this.scControls.SplitterWidth = 1;
            this.scControls.TabIndex = 0;
            // 
            // scTimelineSplit
            // 
            this.scTimelineSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scTimelineSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTimelineSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scTimelineSplit.Location = new System.Drawing.Point(0, 0);
            this.scTimelineSplit.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.scTimelineSplit.Name = "scTimelineSplit";
            // 
            // scTimelineSplit.Panel1
            // 
            this.scTimelineSplit.Panel1.Controls.Add(this.scRackTitles);
            this.scTimelineSplit.Panel1MinSize = 200;
            // 
            // scTimelineSplit.Panel2
            // 
            this.scTimelineSplit.Panel2.AutoScroll = true;
            this.scTimelineSplit.Panel2.Controls.Add(this.scRacksTrackhead);
            this.scTimelineSplit.Panel2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scTimelineSplit_Panel2_Scroll);
            this.scTimelineSplit.Panel2MinSize = 220;
            this.scTimelineSplit.Size = new System.Drawing.Size(971, 486);
            this.scTimelineSplit.SplitterDistance = 200;
            this.scTimelineSplit.SplitterWidth = 2;
            this.scTimelineSplit.TabIndex = 1;
            // 
            // scRackTitles
            // 
            this.scRackTitles.AutoScroll = true;
            this.scRackTitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRackTitles.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scRackTitles.IsSplitterFixed = true;
            this.scRackTitles.Location = new System.Drawing.Point(0, 0);
            this.scRackTitles.Margin = new System.Windows.Forms.Padding(0);
            this.scRackTitles.Name = "scRackTitles";
            this.scRackTitles.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRackTitles.Panel1
            // 
            this.scRackTitles.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.scRackTitles.Panel1.Controls.Add(this.btnTimelineOptions);
            this.scRackTitles.Panel1.Controls.Add(this.lbTimecode);
            this.scRackTitles.Panel1MinSize = 31;
            // 
            // scRackTitles.Panel2
            // 
            this.scRackTitles.Panel2.AutoScroll = true;
            this.scRackTitles.Panel2.Controls.Add(this.lbRackTitles);
            this.scRackTitles.Panel2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scRackTitles_Panel2_Scroll);
            this.scRackTitles.Size = new System.Drawing.Size(198, 484);
            this.scRackTitles.SplitterDistance = 31;
            this.scRackTitles.SplitterWidth = 1;
            this.scRackTitles.TabIndex = 1;
            // 
            // btnTimelineOptions
            // 
            this.btnTimelineOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTimelineOptions.Location = new System.Drawing.Point(173, 0);
            this.btnTimelineOptions.Name = "btnTimelineOptions";
            this.btnTimelineOptions.Size = new System.Drawing.Size(25, 31);
            this.btnTimelineOptions.TabIndex = 1;
            this.btnTimelineOptions.Text = "▼";
            this.ttButtonInfo.SetToolTip(this.btnTimelineOptions, "Options");
            this.btnTimelineOptions.UseVisualStyleBackColor = true;
            // 
            // lbTimecode
            // 
            this.lbTimecode.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbTimecode.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTimecode.Location = new System.Drawing.Point(0, 0);
            this.lbTimecode.Name = "lbTimecode";
            this.lbTimecode.Size = new System.Drawing.Size(173, 31);
            this.lbTimecode.TabIndex = 0;
            this.lbTimecode.Text = "00:00:00.00";
            this.lbTimecode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbRackTitles
            // 
            this.lbRackTitles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbRackTitles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbRackTitles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbRackTitles.FormattingEnabled = true;
            this.lbRackTitles.ItemHeight = 24;
            this.lbRackTitles.Location = new System.Drawing.Point(0, 0);
            this.lbRackTitles.Margin = new System.Windows.Forms.Padding(0);
            this.lbRackTitles.Name = "lbRackTitles";
            this.lbRackTitles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbRackTitles.Size = new System.Drawing.Size(198, 216);
            this.lbRackTitles.TabIndex = 1;
            this.lbRackTitles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbRackTitles_DrawItem);
            this.lbRackTitles.SelectedValueChanged += new System.EventHandler(this.lbRackTitles_SelectedValueChanged);
            // 
            // scRacksTrackhead
            // 
            this.scRacksTrackhead.BackColor = System.Drawing.SystemColors.Control;
            this.scRacksTrackhead.Dock = System.Windows.Forms.DockStyle.Left;
            this.scRacksTrackhead.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scRacksTrackhead.IsSplitterFixed = true;
            this.scRacksTrackhead.Location = new System.Drawing.Point(0, 0);
            this.scRacksTrackhead.Name = "scRacksTrackhead";
            this.scRacksTrackhead.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRacksTrackhead.Panel1
            // 
            this.scRacksTrackhead.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.scRacksTrackhead.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scRacksTrackhead_Panel1_MouseDown);
            this.scRacksTrackhead.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scRacksTrackhead_Panel1_MouseMove);
            this.scRacksTrackhead.Panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scRacksTrackhead_Panel1_MouseUp);
            this.scRacksTrackhead.Panel1MinSize = 32;
            // 
            // scRacksTrackhead.Panel2
            // 
            this.scRacksTrackhead.Panel2.Controls.Add(this.pnlRacks);
            this.scRacksTrackhead.Size = new System.Drawing.Size(330, 484);
            this.scRacksTrackhead.SplitterDistance = 32;
            this.scRacksTrackhead.SplitterWidth = 1;
            this.scRacksTrackhead.TabIndex = 1;
            // 
            // pnlRacks
            // 
            this.pnlRacks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlRacks.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRacks.Location = new System.Drawing.Point(0, 0);
            this.pnlRacks.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRacks.Name = "pnlRacks";
            this.pnlRacks.Size = new System.Drawing.Size(330, 215);
            this.pnlRacks.TabIndex = 0;
            this.pnlRacks.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pnlRacks_Scroll);
            this.pnlRacks.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRacks_Paint);
            this.pnlRacks.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlRacks_MouseClick);
            this.pnlRacks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnlRacks_MouseDoubleClick);
            this.pnlRacks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlRacks_MouseDown);
            this.pnlRacks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlRacks_MouseMove);
            this.pnlRacks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlRacks_MouseUp);
            // 
            // cbSync
            // 
            this.cbSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSync.AutoSize = true;
            this.cbSync.Location = new System.Drawing.Point(218, 14);
            this.cbSync.Name = "cbSync";
            this.cbSync.Size = new System.Drawing.Size(50, 17);
            this.cbSync.TabIndex = 8;
            this.cbSync.Text = "Sync";
            this.cbSync.UseVisualStyleBackColor = true;
            this.cbSync.CheckedChanged += new System.EventHandler(this.cbSync_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(613, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "len";
            // 
            // nudLength
            // 
            this.nudLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLength.Location = new System.Drawing.Point(640, 13);
            this.nudLength.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(65, 20);
            this.nudLength.TabIndex = 9;
            this.nudLength.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudLength.ValueChanged += new System.EventHandler(this.nudLength_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(859, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Zoom";
            // 
            // btnEnd
            // 
            this.btnEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEnd.Image = global::Keyshift.Forms.Properties.Resources.end;
            this.btnEnd.Location = new System.Drawing.Point(182, 7);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(30, 30);
            this.btnEnd.TabIndex = 7;
            this.ttButtonInfo.SetToolTip(this.btnEnd, "Jump to End");
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlay.Image = global::Keyshift.Forms.Properties.Resources.play;
            this.btnPlay.Location = new System.Drawing.Point(75, 7);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(30, 30);
            this.btnPlay.TabIndex = 4;
            this.ttButtonInfo.SetToolTip(this.btnPlay, "Play/Pause");
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnFrontKf
            // 
            this.btnFrontKf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFrontKf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFrontKf.Image = global::Keyshift.Forms.Properties.Resources.frontKf;
            this.btnFrontKf.Location = new System.Drawing.Point(577, 8);
            this.btnFrontKf.Name = "btnFrontKf";
            this.btnFrontKf.Size = new System.Drawing.Size(30, 30);
            this.btnFrontKf.TabIndex = 11;
            this.ttButtonInfo.SetToolTip(this.btnFrontKf, "Next Keyframe");
            this.btnFrontKf.UseVisualStyleBackColor = true;
            this.btnFrontKf.Click += new System.EventHandler(this.btnFrontKf_Click);
            // 
            // btnAdvance
            // 
            this.btnAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdvance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdvance.Image = global::Keyshift.Forms.Properties.Resources.frameadvance;
            this.btnAdvance.Location = new System.Drawing.Point(147, 7);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(30, 30);
            this.btnAdvance.TabIndex = 6;
            this.ttButtonInfo.SetToolTip(this.btnAdvance, "Advance Frame");
            this.btnAdvance.UseVisualStyleBackColor = true;
            this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // btnBackKf
            // 
            this.btnBackKf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackKf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBackKf.Image = global::Keyshift.Forms.Properties.Resources.behindKf;
            this.btnBackKf.Location = new System.Drawing.Point(541, 8);
            this.btnBackKf.Name = "btnBackKf";
            this.btnBackKf.Size = new System.Drawing.Size(30, 30);
            this.btnBackKf.TabIndex = 10;
            this.ttButtonInfo.SetToolTip(this.btnBackKf, "Previous Keyframe");
            this.btnBackKf.UseVisualStyleBackColor = true;
            this.btnBackKf.Click += new System.EventHandler(this.btnBackKf_Click);
            // 
            // btnStepback
            // 
            this.btnStepback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStepback.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStepback.Image = global::Keyshift.Forms.Properties.Resources.framestepback;
            this.btnStepback.Location = new System.Drawing.Point(39, 7);
            this.btnStepback.Name = "btnStepback";
            this.btnStepback.Size = new System.Drawing.Size(30, 30);
            this.btnStepback.TabIndex = 3;
            this.ttButtonInfo.SetToolTip(this.btnStepback, "Stepback Frame");
            this.btnStepback.UseVisualStyleBackColor = true;
            this.btnStepback.Click += new System.EventHandler(this.btnStepback_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Image = global::Keyshift.Forms.Properties.Resources.stop;
            this.btnStop.Location = new System.Drawing.Point(111, 7);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(30, 30);
            this.btnStop.TabIndex = 5;
            this.ttButtonInfo.SetToolTip(this.btnStop, "Stop");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStart.Image = global::Keyshift.Forms.Properties.Resources.begin;
            this.btnStart.Location = new System.Drawing.Point(4, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(30, 30);
            this.btnStart.TabIndex = 2;
            this.ttButtonInfo.SetToolTip(this.btnStart, "Jump to Start");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnNudgeR
            // 
            this.btnNudgeR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNudgeR.Image = global::Keyshift.Forms.Properties.Resources.nudgeKfRight;
            this.btnNudgeR.Location = new System.Drawing.Point(819, 7);
            this.btnNudgeR.Name = "btnNudgeR";
            this.btnNudgeR.Size = new System.Drawing.Size(30, 30);
            this.btnNudgeR.TabIndex = 17;
            this.btnNudgeR.Text = " ";
            this.ttButtonInfo.SetToolTip(this.btnNudgeR, "Move selected keyframes one frame forward");
            this.btnNudgeR.UseVisualStyleBackColor = true;
            this.btnNudgeR.Click += new System.EventHandler(this.btnNudgeR_Click);
            // 
            // btnNudgeL
            // 
            this.btnNudgeL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNudgeL.Image = global::Keyshift.Forms.Properties.Resources.nudgeKfLeft;
            this.btnNudgeL.Location = new System.Drawing.Point(783, 8);
            this.btnNudgeL.Name = "btnNudgeL";
            this.btnNudgeL.Size = new System.Drawing.Size(30, 30);
            this.btnNudgeL.TabIndex = 16;
            this.btnNudgeL.Text = " ";
            this.ttButtonInfo.SetToolTip(this.btnNudgeL, "Move selected keyframes one frame back");
            this.btnNudgeL.UseVisualStyleBackColor = true;
            this.btnNudgeL.Click += new System.EventHandler(this.btnNudgeL_Click);
            // 
            // btnRemoveKf
            // 
            this.btnRemoveKf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveKf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveKf.Image = global::Keyshift.Forms.Properties.Resources.deleteKf;
            this.btnRemoveKf.Location = new System.Drawing.Point(747, 8);
            this.btnRemoveKf.Name = "btnRemoveKf";
            this.btnRemoveKf.Size = new System.Drawing.Size(30, 30);
            this.btnRemoveKf.TabIndex = 15;
            this.btnRemoveKf.Text = " ";
            this.ttButtonInfo.SetToolTip(this.btnRemoveKf, "Remove Keyframes\r\n\r\n(Removes selected, if none selected, those in active racks un" +
        "der the track head.)");
            this.btnRemoveKf.UseVisualStyleBackColor = true;
            this.btnRemoveKf.Click += new System.EventHandler(this.btnRemoveKf_Click);
            // 
            // btnAddKf
            // 
            this.btnAddKf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddKf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddKf.Image = global::Keyshift.Forms.Properties.Resources.addKf;
            this.btnAddKf.Location = new System.Drawing.Point(711, 7);
            this.btnAddKf.Name = "btnAddKf";
            this.btnAddKf.Size = new System.Drawing.Size(30, 30);
            this.btnAddKf.TabIndex = 14;
            this.ttButtonInfo.SetToolTip(this.btnAddKf, "Add Keyframes from Current Values");
            this.btnAddKf.UseVisualStyleBackColor = true;
            this.btnAddKf.Click += new System.EventHandler(this.btnAddKf_Click);
            // 
            // tbZoom
            // 
            this.tbZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoom.AutoSize = false;
            this.tbZoom.Location = new System.Drawing.Point(899, 9);
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(69, 29);
            this.tbZoom.TabIndex = 18;
            this.tbZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbZoom.Scroll += new System.EventHandler(this.tbZoom_Scroll);
            // 
            // cmsKeyframeSettings
            // 
            this.cmsKeyframeSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLinear,
            this.tsmFast,
            this.tsmSlow,
            this.tsmSmooth,
            this.tsmSharp,
            this.tsmHold,
            this.toolStripSeparator1,
            this.tsmBezier,
            this.toolStripSeparator2});
            this.cmsKeyframeSettings.Name = "cmsKeyframeSettings";
            this.cmsKeyframeSettings.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsKeyframeSettings.Size = new System.Drawing.Size(117, 170);
            // 
            // tsmLinear
            // 
            this.tsmLinear.Name = "tsmLinear";
            this.tsmLinear.Size = new System.Drawing.Size(116, 22);
            this.tsmLinear.Text = "Linear";
            // 
            // tsmFast
            // 
            this.tsmFast.Name = "tsmFast";
            this.tsmFast.Size = new System.Drawing.Size(116, 22);
            this.tsmFast.Text = "Fast";
            // 
            // tsmSlow
            // 
            this.tsmSlow.Name = "tsmSlow";
            this.tsmSlow.Size = new System.Drawing.Size(116, 22);
            this.tsmSlow.Text = "Slow";
            // 
            // tsmSmooth
            // 
            this.tsmSmooth.Name = "tsmSmooth";
            this.tsmSmooth.Size = new System.Drawing.Size(116, 22);
            this.tsmSmooth.Text = "Smooth";
            // 
            // tsmSharp
            // 
            this.tsmSharp.Name = "tsmSharp";
            this.tsmSharp.Size = new System.Drawing.Size(116, 22);
            this.tsmSharp.Text = "Sharp";
            // 
            // tsmHold
            // 
            this.tsmHold.Name = "tsmHold";
            this.tsmHold.Size = new System.Drawing.Size(116, 22);
            this.tsmHold.Text = "Hold";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // tsmBezier
            // 
            this.tsmBezier.Name = "tsmBezier";
            this.tsmBezier.Size = new System.Drawing.Size(116, 22);
            this.tsmBezier.Text = "Bezier...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // cmsWhy
            // 
            this.cmsWhy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whyCantIControlTheTimelineWithTheMouseToolStripMenuItem});
            this.cmsWhy.Name = "cmsWhy";
            this.cmsWhy.Size = new System.Drawing.Size(333, 26);
            // 
            // whyCantIControlTheTimelineWithTheMouseToolStripMenuItem
            // 
            this.whyCantIControlTheTimelineWithTheMouseToolStripMenuItem.Name = "whyCantIControlTheTimelineWithTheMouseToolStripMenuItem";
            this.whyCantIControlTheTimelineWithTheMouseToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.whyCantIControlTheTimelineWithTheMouseToolStripMenuItem.Text = "Why can\'t I control the Timeline with the mouse?";
            this.whyCantIControlTheTimelineWithTheMouseToolStripMenuItem.Click += new System.EventHandler(this.whyCantIControlTheTimelineWithTheMouseToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TimelineRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.scControls);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(420, 128);
            this.Name = "TimelineRenderer";
            this.Size = new System.Drawing.Size(971, 523);
            this.scControls.Panel1.ResumeLayout(false);
            this.scControls.Panel2.ResumeLayout(false);
            this.scControls.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scControls)).EndInit();
            this.scControls.ResumeLayout(false);
            this.scTimelineSplit.Panel1.ResumeLayout(false);
            this.scTimelineSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTimelineSplit)).EndInit();
            this.scTimelineSplit.ResumeLayout(false);
            this.scRackTitles.Panel1.ResumeLayout(false);
            this.scRackTitles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scRackTitles)).EndInit();
            this.scRackTitles.ResumeLayout(false);
            this.scRacksTrackhead.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scRacksTrackhead)).EndInit();
            this.scRacksTrackhead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            this.cmsKeyframeSettings.ResumeLayout(false);
            this.cmsWhy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scControls;
        private System.Windows.Forms.SplitContainer scTimelineSplit;
        private ScrollableSplitContainer scRackTitles;
        private System.Windows.Forms.Label lbTimecode;
        private System.Windows.Forms.ListBox lbRackTitles;
        private System.Windows.Forms.TrackBar tbZoom;
        private System.Windows.Forms.Panel pnlRacks;
        private System.Windows.Forms.Button btnNudgeR;
        private System.Windows.Forms.Button btnNudgeL;
        private System.Windows.Forms.Button btnRemoveKf;
        private System.Windows.Forms.Button btnAddKf;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ToolTip ttButtonInfo;
        private System.Windows.Forms.Button btnAdvance;
        private System.Windows.Forms.Button btnStepback;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ContextMenuStrip cmsKeyframeSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFrontKf;
        private System.Windows.Forms.Button btnBackKf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.CheckBox cbSync;
        private System.Windows.Forms.ContextMenuStrip cmsWhy;
        private System.Windows.Forms.ToolStripMenuItem whyCantIControlTheTimelineWithTheMouseToolStripMenuItem;
        private System.Windows.Forms.SplitContainer scRacksTrackhead;
        private System.Windows.Forms.Button btnTimelineOptions;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmLinear;
        private System.Windows.Forms.ToolStripMenuItem tsmFast;
        private System.Windows.Forms.ToolStripMenuItem tsmSlow;
        private System.Windows.Forms.ToolStripMenuItem tsmSmooth;
        private System.Windows.Forms.ToolStripMenuItem tsmSharp;
        private System.Windows.Forms.ToolStripMenuItem tsmHold;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmBezier;
    }
}
