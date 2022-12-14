﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keyshift.Core.Classes;
using Keyshift.Forms.Controls.VisualAid;
using Keyshift.Forms.Properties;

namespace Keyshift.Forms.Controls
{
    public partial class TimelineRenderer : UserControl
    {
        private const int WHEEL_DELTA = 120;
        private const int KEYFRAME_WIDTH = 24; // TODO: Change this size to be dynamic especially vs. HiDPI

        private Brush headerBrush = new SolidBrush(Color.FromArgb(150, 0, 0, 0));
        private Brush frameNumBrush = new SolidBrush(Color.White);

        private Brush checkerboardBrush =
            new HatchBrush(HatchStyle.LargeCheckerBoard, Color.FromArgb(128, 255, 255, 255), Color.FromArgb(64, 255, 255, 255));
        private Brush trackevenBrush = new SolidBrush(Color.FromArgb(0x717171));
        private Brush trackoddBrush = new SolidBrush(Color.FromArgb(0x5b5b5b));
        private Brush trackHeadBrush = new SolidBrush(Color.White);

        private Pen trackHeadPen = new Pen(Color.Black);
        private Pen trackBodyPen = new Pen(Color.FromArgb(255, 82, 135, 255));
        private Pen frameCountPen = new Pen(Color.LightGray, 1);
        private Pen selectedKeyframePen = new Pen(Color.White, 2);

        private Pen selectionpen = new Pen(Color.FromArgb(255, 60, 90, 255));
        private Brush selectionbrush = new SolidBrush(Color.FromArgb(128, 60, 90, 255));

        private BindingSource _tlBs = new BindingSource();
        private bool _initialized;

        private int frameSizeInPixels;

        private Timeline _tl;

        private bool _movingTrackhead;
        private bool _draggingKeyframes;
        private bool _selecting;
        private bool _multiSelect;
        private bool _invertiveSelection;
        private int[] _selectionFrameMargins = new[] { 0, 0, 0 }; // The third is the index of the track
        private Point _startingMousePos;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public TimelineRenderer() {
            InitializeComponent();
            _initialized = false;
        }

        public TimelineRenderer(Timeline tl)
        {
            InitializeComponent();
            InitializeTimeline(tl);
        }

        public void InitializeTimeline(Timeline tl) {
            
            
            _tl = tl;
            _tlBs.DataSource = _tl.KeyframeRacks;

            tsmLinear.Click += (a, b) => { _tl.SetSelectedKeyframeInterpolation(KeyframeType.Linear); _tl.CommitAllStaged(); };
            tsmSlow.Click += (a, b) => { _tl.SetSelectedKeyframeInterpolation(KeyframeType.Slow); _tl.CommitAllStaged(); };
            tsmFast.Click += (a, b) => { _tl.SetSelectedKeyframeInterpolation(KeyframeType.Fast); _tl.CommitAllStaged(); };
            tsmSharp.Click += (a, b) => { _tl.SetSelectedKeyframeInterpolation(KeyframeType.Sharp); _tl.CommitAllStaged(); };
            tsmSmooth.Click += (a, b) => { _tl.SetSelectedKeyframeInterpolation(KeyframeType.Smooth); _tl.CommitAllStaged(); };
            tsmHold.Click += (a, b) => { _tl.SetSelectedKeyframeInterpolation(KeyframeType.Hold); _tl.CommitAllStaged(); };

            lbRackTitles.DataSource = _tlBs;
            lbRackTitles.DisplayMember = "Value";
            // Otherwise crashes
            if (_tl.KeyframeRacks.Count != 0) {
                lbRackTitles.ValueMember = "Key";
            }

            tbZoom.Maximum = pnlRacks.Width / 3;
            tbZoom.Value = tbZoom.Maximum;
            tbZoom.Minimum = Math.Min(_tl.Length / pnlRacks.Width, KEYFRAME_WIDTH / 2);

            frameSizeInPixels = tbZoom.Value;
            lbRackTitles.Height = lbRackTitles.ItemHeight * (lbRackTitles.Items.Count + 1);
            pnlRacks.Height = scRackTitles.Panel1.Height + lbRackTitles.Height + 4;

            scRackTitles.Panel2.AutoScrollPosition = new Point(0, 0);
            scRackTitles.Panel2.VerticalScroll.Maximum = 999;
            _tl.OnTrackheadChanged += (tl, e) => RedrawOldNewFrame(e.PreviousFrame, e.Frame);
            _tl.OnChangesCancelled += (tl, e) => Redraw();
            _tl.OnChangesCommitted += (tl, e) => Redraw();
            _tl.OnKeyframeBulkChanged += (tl, e) => Redraw();
            _tl.OnKeyframeChanged += (tl, e) => Redraw();
            _tl.OnTimelineLoaded += (tl, e) => Redraw();
            _tl.Length = (int)nudLength.Value;
            _tl.OnTracksChanged += (tl, e) =>
            {
                _tlBs.ResetBindings(true);
                _tlBs.DataSource = null;
                _tlBs.DataSource = _tl.KeyframeRacks;

                if (lbRackTitles.ValueMember != "Key") {
                    lbRackTitles.ValueMember = "Key";
                }
                

                Redraw();
                ResizeRacks();
            };
            pnlRacks.Width = _tl.Length * frameSizeInPixels;

            trackHeadPen.Width = 1;
            trackBodyPen.Width = 2;

            scTimelineSplit.Panel2.VerticalScroll.Enabled = true;
            scTimelineSplit.Panel2.HorizontalScroll.Enabled = false;

            scRackTitles.Panel2.MouseWheel += Panel2_MouseWheel;
            scRacksTrackhead.Panel2.VerticalScroll.Maximum = int.MaxValue / 2;

            AdjustTimelineWidth();
            _initialized = true;
            Redraw();
            ResizeRacks();
        }

        private void Panel2_MouseWheel(object sender, MouseEventArgs e)
        {
            SnapVerticalScrollbars(scRackTitles.Panel2.VerticalScroll.Value);
        }

        public void Redraw()
        {
            if (!IsHandleCreated) return;
            BeginInvoke(new MethodInvoker(() =>
            {
                lbTimecode.Text = _tl.TimecodeString();
                scRacksTrackhead.Panel1.Invalidate();
                pnlRacks.Invalidate();
            }));

        }

        private void RedrawOldNewFrame(int oldFrm, int newFrm)
        {
            RedrawOnFrame(oldFrm);
            RedrawOnFrame(newFrm);
        }

        private void RedrawOnFrame(int frame)
        {
            if (!IsHandleCreated) return;

            frameSizeInPixels = tbZoom.Value;
            int leftBoundary = Math.Max(0, Math.Min(frame * frameSizeInPixels - (int)Math.Round(frameSizeInPixels * 0.5), frame * frameSizeInPixels - KEYFRAME_WIDTH));
            int rightBoundary = Math.Min(frameSizeInPixels * _tl.Length, Math.Max(frame * frameSizeInPixels + (int)Math.Round(frameSizeInPixels * 0.5), frame * frameSizeInPixels + KEYFRAME_WIDTH));

            BeginInvoke(new MethodInvoker(() =>
            {
                lbTimecode.Text = _tl.TimecodeString();
                scRacksTrackhead.Panel1.Invalidate(new Rectangle(leftBoundary, 0, rightBoundary - leftBoundary, scRacksTrackhead.Height));
                pnlRacks.Invalidate(new Rectangle(leftBoundary, 0, rightBoundary - leftBoundary, pnlRacks.Height));
            }));

        }

        public void ResizeRacks()
        {
            if (!IsHandleCreated) return;
            BeginInvoke(new MethodInvoker(() =>
            {
                pnlRacks.Height = lbRackTitles.ItemHeight * (lbRackTitles.Items.Count + 1);
                lbRackTitles.Height = pnlRacks.Height;
            }));
        }

        void AdjustTimelineWidth()
        {
            frameSizeInPixels = tbZoom.Value;
            scRacksTrackhead.Width = _tl.Length * frameSizeInPixels + (frameSizeInPixels) + (int)(Font.Height * _tl.Length.ToString().Length / 2);
            pnlRacks.Width = scRacksTrackhead.Width;
        }

        private void pnlRacks_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = e.Graphics)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                int headerHeight = lbRackTitles.ItemHeight;
                g.Clear(Color.DarkSlateGray);
                g.FillRectangle(checkerboardBrush, new Rectangle(0, headerHeight + 5, pnlRacks.Width, pnlRacks.Height - headerHeight));

                if (!_initialized) {
                    g.FillRectangle(Brushes.LightGray, (int)(pnlRacks.Width * 0.1), (int)(pnlRacks.Height * 0.1), (int)(pnlRacks.Width * 0.8), (int)(pnlRacks.Height * 0.8));
                    // Big X shape
                    g.DrawLine(new Pen(Color.FromArgb(100,255,0,0), 3), (int)(scRacksTrackhead.Panel2.Width * 0.1), (int)(pnlRacks.Height * 0.1), (int)(scRacksTrackhead.Panel2.Width * 0.9), (int)(pnlRacks.Height * 0.9));
                    g.DrawLine(new Pen(Color.FromArgb(100, 255, 0, 0), 3), (int)(scRacksTrackhead.Panel2.Width * 0.1), (int)(pnlRacks.Height * 0.9), (int)(scRacksTrackhead.Panel2.Width * 0.9), (int)(pnlRacks.Height * 0.1));
                    SizeF stringmetrics = g.MeasureString("NO TIMELINE", Font);
                    g.DrawString("NO TIMELINE", new Font(Font.FontFamily, 16, FontStyle.Bold), Brushes.Red, (pnlRacks.Width/2) - stringmetrics.Width, (pnlRacks.Height / 2) - stringmetrics.Height);
                    return;
                }


                // Flatten the keyframe racks to an array
                // TODO: Potentially give the user a way to organize the racks however they want (does not affect order in Timeline)
                KeyValuePair<string, KeyframeRack>[] krArray = _tl.KeyframeRacks.ToArray();

                // Draw the racks with alternating colors
                for (int i = 0; i < krArray.Count(); i++)
                {
                    g.FillRectangle((i % 2 == 0 ? new SolidBrush(Color.FromArgb(97, 97, 97)) : new SolidBrush(Color.FromArgb(50, 50, 50))), new Rectangle(0, 0 + (i * 24), pnlRacks.Width, 24));

                }

                // Why do I repeat the same loop? Painter's algorithm
                // Keyframes go over the racks and so any modifiers (like selection)
                for (int i = 0; i < krArray.Count(); i++)
                {
                    foreach (Keyframe kf in krArray[i].Value.OrderedGenericList)
                    {

                        if (_tl.UncommittedRackChanges[krArray[i].Key] != null)
                        {
                            // When there's uncommitted, draw only already committed
                            if (!_tl.UncommittedRackChanges[krArray[i].Key].Keyframes.Contains(kf))
                            {
                                DrawNormalKeyframe(g, kf, i);
                            }

                        }
                        else
                        {
                            DrawNormalKeyframe(g, kf, i);
                        }

                        // Uncommitted keyframes here (Above regular keyframes)

                    }

                    if (_tl.UncommittedRackChanges[krArray[i].Key] != null)
                    {
                        // Second iteration (only uncommitted)
                        foreach (Keyframe kf in krArray[i].Value.OrderedGenericList)
                        {
                            // When there's uncommitted, draw only already committed
                            if (_tl.UncommittedRackChanges[krArray[i].Key].Keyframes.Contains(kf))
                            {
                                DrawUncommittedKeyframe(g, kf, krArray, i);
                            }
                        }
                    }


                    // Selection box
                    if (_selecting && i == _selectionFrameMargins[2])
                    {
                        g.FillRectangle(selectionbrush, _selectionFrameMargins[0] * frameSizeInPixels, i * KEYFRAME_WIDTH,
                            (_selectionFrameMargins[1] * frameSizeInPixels) - (_selectionFrameMargins[0] * frameSizeInPixels), KEYFRAME_WIDTH);
                        g.DrawRectangle(selectionpen, _selectionFrameMargins[0] * frameSizeInPixels, i * KEYFRAME_WIDTH,
                            (_selectionFrameMargins[1] * frameSizeInPixels) - (_selectionFrameMargins[0] * frameSizeInPixels), KEYFRAME_WIDTH);
                    }

                }

                g.DrawLine(trackBodyPen, _tl.TrackheadPosition * frameSizeInPixels, 0, _tl.TrackheadPosition * frameSizeInPixels, pnlRacks.Height);
                g.SmoothingMode = SmoothingMode.HighSpeed;
            }
        }

        private void DrawNormalKeyframe(Graphics g, Keyframe kf, int trackIndex)
        {
            VectorShape vecChosen = KeyframeShapes.Shapes[kf.InterpolationType];

            GraphicsPath pathChosenTransformed = (GraphicsPath)vecChosen.Path.Clone();
            Matrix tlChosenMatrix = new Matrix();
            tlChosenMatrix.Translate((kf.Position * frameSizeInPixels) - KEYFRAME_WIDTH / 2, (trackIndex * KEYFRAME_WIDTH));
            pathChosenTransformed.Transform(tlChosenMatrix);

            g.FillPath(vecChosen.Brush, pathChosenTransformed);
            g.DrawPath((_tl.SelectedKeyframes.Contains(kf) ? selectedKeyframePen : vecChosen.Pen), pathChosenTransformed);
        }
        private void DrawUncommittedKeyframe(Graphics g, Keyframe kf, KeyValuePair<string, KeyframeRack>[] krArray, int trackIndex)
        {
            int delta = _tl.UncommittedRackChanges[krArray[trackIndex].Key].Delta;

            VectorShape vec2Draw = (_tl.UncommittedRackChanges[krArray[trackIndex].Key].NewInterpolation == null
                ? KeyframeShapes.Shapes[kf.InterpolationType]
                : KeyframeShapes.Shapes[
                    (KeyframeType)_tl.UncommittedRackChanges[krArray[trackIndex].Key].NewInterpolation]);

            GraphicsPath pathTransformed = (GraphicsPath)vec2Draw.Path.Clone();
            Matrix tlMatrix = new Matrix();
            tlMatrix.Translate((kf.Position + delta) * frameSizeInPixels - KEYFRAME_WIDTH / 2, (trackIndex * KEYFRAME_WIDTH) - KEYFRAME_WIDTH * 0.25f);
            pathTransformed.Transform(tlMatrix);

            g.FillPath(vec2Draw.Brush, pathTransformed);
            g.DrawPath(selectedKeyframePen, pathTransformed);

            g.DrawImage(Resources.moveKeyframes, (int)((kf.Position + delta) * frameSizeInPixels) - KEYFRAME_WIDTH / 2, (trackIndex * 24));
        }
        private void tbZoom_Scroll(object sender, EventArgs e)
        {
            AdjustTimelineWidth();
            Redraw();
        }

        private void scRackTitles_Panel2_Scroll(object sender, ScrollEventArgs e)
        {
            SnapVerticalScrollbars(e.NewValue);
        }

        private void SnapVerticalScrollbars(int value)
        {
            int newVal = (int)Math.Floor(value / (float)lbRackTitles.ItemHeight);
            newVal *= lbRackTitles.ItemHeight;
            // I have NO idea why, but if the scrollpanel is at 0 then it will literally just not scroll properly.
            // Probably some obscure Winforms problem
            scRacksTrackhead.Panel2.VerticalScroll.Value = Math.Max(1, newVal);
            scRackTitles.Panel2.VerticalScroll.Value = Math.Max(1, newVal);
            scRacksTrackhead.Panel2.PerformLayout();
        }

        private void lbRackTitles_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;
            if (_tl.KeyframeRacks.Count == 0) return;

            _tl.SelectedRacks.Clear();
            _tl.SelectedRacks.AddRange(lbRackTitles.SelectedItems.Cast<KeyValuePair<string, KeyframeRack>>().Select(x => x.Key));
        }

        private void lbRackTitles_DrawItem(object sender, DrawItemEventArgs e) {
            if (!_initialized) return;
            if (_tl.KeyframeRacks.Count == 0) return;

            using (Graphics g = e.Graphics)
            {
                bool isSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
                e.DrawBackground();

                SolidBrush blackbrush = new(Color.Black);
                SolidBrush whitebrush = new(Color.White);

                g.DrawString(lbRackTitles.Items.Cast<KeyValuePair<string, KeyframeRack>>().ToArray()[e.Index].Value.Name, Font, (isSelected ? whitebrush : blackbrush), 12f, e.Bounds.Top + e.Bounds.Height / 2f - Font.Height / 2);
                e.DrawFocusRectangle();
            }
        }

        private void btnAddKf_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.AddAllCurrentValuesToRacks();
            Redraw();
        }

        private void btnNudgeL_Click(object sender, EventArgs e) {
            if (!_initialized) return;
            _tl.History?.Undo();
            Redraw();
        }

        private void btnNudgeR_Click(object sender, EventArgs e) {
            if (!_initialized) return;
            _tl.History?.Redo();
            Redraw();
        }

        private void scTimelineSplit_Panel2_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void btnRemoveKf_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            if (_tl.StagedKeyframesPresent)
            {
                _tl.DeleteAllStaged();
            }
            else
            {
                _tl.DeleteKeyframesAtTrackhead();
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.Playing = !_tl.Playing;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.Playing = false;
            _tl.TrackheadPosition = 0;
        }

        private void btnStepback_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.TrackheadPosition--;
        }

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.TrackheadPosition++;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.TrackheadPosition = 0;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.TrackheadPosition = _tl.Length;
        }

        private void btnBackKf_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.TrackheadPosition = _tl.GetImmediateKeyframePosition(false);
        }

        private void btnFrontKf_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.TrackheadPosition = _tl.GetImmediateKeyframePosition(true);
        }

        private void cbSync_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.Synchronize = cbSync.Checked;
        }

        private void nudLength_ValueChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;
            _tl.Length = (int)((NumericUpDown)sender).Value;
        }

        private void whyCantIControlTheTimelineWithTheMouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void pnlRacks_MouseClick(object sender, MouseEventArgs e) {
            if (!_initialized) return;

            if (e.Button == MouseButtons.Right)
            {
                if (_draggingKeyframes)
                {
                    _tl.ClearStagedKeyframes();
                    _draggingKeyframes = false;
                }
                else
                {
                    cmsKeyframeSettings.Show(pnlRacks, e.Location);
                }
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            int headerHeight = scRacksTrackhead.Panel1.Height;
            using (Graphics g = e.Graphics)
            {

                // Clear background of panel
                g.Clear(Color.DarkSlateGray);
                Font frameFont = new Font(Font.FontFamily, 8, FontStyle.Regular);

                // Calculate the separation between framenumbers
                int framenumModulo = (int)Math.Max(Math.Round(g.MeasureString("999", frameFont).Width / frameSizeInPixels * 1.5), 1);


                // Make header and framelines
                g.FillRectangle(headerBrush, new Rectangle(0, 0, pnlRacks.Width, headerHeight));
                if (!_initialized) return;
                for (int i = 0; i <= _tl.Length; i++)
                {
                    g.DrawLine(frameCountPen, 0 + frameSizeInPixels * i, headerHeight - 1, 0 + frameSizeInPixels * i, (i % framenumModulo == 0 ? headerHeight - 13 : headerHeight - 4));
                    if (i % framenumModulo == 0)
                    {
                        SizeF nmeasure = g.MeasureString(i.ToString(), frameFont);
                        g.DrawString(i.ToString(), frameFont, frameNumBrush, i * frameSizeInPixels, headerHeight - 10 - (nmeasure.Height / 2));
                    }
                }

                GraphicsPath trackheadPath = new GraphicsPath();
                trackheadPath.AddPolygon(new PointF[]
                {
                    new(_tl.TrackheadPosition * frameSizeInPixels - 5, 14),
                    new(_tl.TrackheadPosition * frameSizeInPixels - 5, 25),
                    new(_tl.TrackheadPosition * frameSizeInPixels, 32),
                    new(_tl.TrackheadPosition * frameSizeInPixels + 6, 25),
                    new(_tl.TrackheadPosition * frameSizeInPixels + 6, 14)
                });
                g.FillPath(trackHeadBrush, trackheadPath);
                g.DrawPath(trackHeadPen, trackheadPath);

                g.DrawLine(frameCountPen, 0, headerHeight, pnlRacks.Width, headerHeight);
            }
        }

        private void pnlRacks_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private bool IsPointOnFrameKeyframeBounds(Point place)
        {
            int x = (int)Math.Round(place.X / (float)frameSizeInPixels);
            return (place.X > (x * frameSizeInPixels) - KEYFRAME_WIDTH / 2 && (x * frameSizeInPixels) + KEYFRAME_WIDTH / 2 > place.X);
        }

        private void scRacksTrackhead_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_initialized) return;
            if (!_movingTrackhead)
            {
                _movingTrackhead = true;
                _startingMousePos = new Point(e.X, e.Y);
            }

            int x = (int)Math.Round(e.X / (float)frameSizeInPixels);
            _tl.TrackheadPosition = x;
        }

        private Keyframe IsMouseOnKeyframe(Point place)
        {
            int x = (int)Math.Round(place.X / (float)frameSizeInPixels);
            int index = (int)Math.Floor(place.Y / (float)KEYFRAME_WIDTH);
            if (!IsPointOnFrameKeyframeBounds(place)) return null;
            if (index > _tl.KeyframeRacks.Count - 1 || index < 0) return null;
            return _tl.KeyframeRacks.Values.ToArray()[index].GetKeyframeAtPosition(x);
        }

        private void scRacksTrackhead_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_initialized) return;
            if (!_movingTrackhead) return;

            int x = (int)Math.Round(e.X / (float)frameSizeInPixels);
            _tl.TrackheadPosition = x;
        }

        private void scRacksTrackhead_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_initialized) return;
            if (!_movingTrackhead) return;
            _movingTrackhead = false;
        }

        private void pnlRacks_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_initialized) return;
            if (e.Button == MouseButtons.Left)
            {


                if (_draggingKeyframes)
                {
                    int deltaX =
                        (int)Math.Round((e.Location.X - ((int)Math.Round(_startingMousePos.X / (float)frameSizeInPixels) *
                                                 frameSizeInPixels)) /
                                         (float)frameSizeInPixels);

                    if (e.Location != _startingMousePos && !_tl.StagedKeyframesPresent)
                    {
                        if (_tl.SelectedKeyframes.Count == 0) {
                            _tl.SelectedKeyframes.Add(IsMouseOnKeyframe(e.Location));
                        }
                        
                        _tl.StageSelectedKeyframes();
                    }
                    _tl.SetAllDeltas(deltaX);
                    Redraw();
                    return;
                }

                if (_movingTrackhead)
                {
                    int x = (int)Math.Round(e.X / (float)frameSizeInPixels);
                    _tl.TrackheadPosition = x;
                }

                if (_selecting)
                {
                    int start = (int)Math.Round(_startingMousePos.X / (float)frameSizeInPixels);
                    int end = (int)Math.Round(e.Location.X / (float)frameSizeInPixels);
                    _selectionFrameMargins[0] = Math.Max(0, Math.Min(start, end));
                    _selectionFrameMargins[1] = Math.Min(_tl.Length, Math.Max(start, end));
                    Redraw();
                }
            }

            Cursor = IsMouseOnKeyframe(e.Location) != null ? Cursors.Hand : Cursors.Default;
        }

        private void pnlRacks_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_initialized) return;
            Keyframe underMouse = IsMouseOnKeyframe(e.Location);
            _startingMousePos = e.Location;

            if (e.Button == MouseButtons.Left)
            {
                if (underMouse != null)
                {
                    if (!_tl.SelectedKeyframes.Contains(underMouse))
                    {
                        _tl.SelectedKeyframes.Clear();
                    }
                    _draggingKeyframes = true;
                }
                else
                {
                    _tl.SelectedKeyframes.Clear();
                    _movingTrackhead = true;
                    _selecting = true;
                    _selectionFrameMargins[2] = (int)Math.Floor(e.Location.Y / (float)KEYFRAME_WIDTH);
                }
            }

        }

        private void pnlRacks_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_initialized) return;
            if (e.Button == MouseButtons.Left)
            {


                if (_draggingKeyframes)
                {
                    _draggingKeyframes = false;
                    _tl.CommitAllStaged();
                }
                _movingTrackhead = false;

                if (_selecting)
                {
                    if (_selectionFrameMargins[2] < _tl.KeyframeRacks.Count && _selectionFrameMargins[2] >= 0)
                    {
                        string chosenRackId = _tl.KeyframeRacks.FirstOrDefault(x =>
                            x.Value == _tl.KeyframeRacks.Values.ToArray()[_selectionFrameMargins[2]]).Key;
                        Keyframe[] selected = _tl.GetKeyframesFromRackInRange(chosenRackId, _selectionFrameMargins[0], _selectionFrameMargins[1]);
                        _tl.SelectedKeyframes.AddRange(selected);
                    }
                    _selectionFrameMargins = new[] { 0, 0, 0 };
                    _selecting = false;
                }

                Keyframe possibleKeyframe = IsMouseOnKeyframe(e.Location);

                if (e.Location == _startingMousePos)
                {
                    int frame = (int)Math.Round(e.Location.X / (float)frameSizeInPixels);
                    _tl.TrackheadPosition = frame;
                    if (possibleKeyframe != null)
                    {
                        if (!_multiSelect)
                        {
                            _tl.SelectedKeyframes.Clear();
                        }

                        if (_tl.SelectedKeyframes.Contains(possibleKeyframe))
                        {
                            if (_invertiveSelection) _tl.SelectedKeyframes.Remove(possibleKeyframe);
                        }
                        else
                        {
                            _tl.SelectedKeyframes.Add(possibleKeyframe);
                        }
                    }
                    else
                    {
                        _tl.SelectedKeyframes.Clear();
                    }
                }
            }
            cmsKeyframeSettings.Enabled = (_tl.SelectedKeyframes.Count > 0);
            Redraw();
        }

        private void pnlRacks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!_initialized) return;
            if (_selectionFrameMargins[2] > _tl.KeyframeRacks.Count && _selectionFrameMargins[2] < 0) return;

            Keyframe possibleKeyframe = IsMouseOnKeyframe(e.Location);
            int frame = (int)Math.Round(e.Location.X / (float)frameSizeInPixels);
            if (possibleKeyframe == null)
            {
                int trkIndex = (int)Math.Floor(e.Location.Y / (float)KEYFRAME_WIDTH);
                _tl.KeyframeRacks.Values.ToArray()[trkIndex].AddCurrentStateAtPosition(frame);
            }
        }
    }
}
