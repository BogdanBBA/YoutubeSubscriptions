using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeSubscriptions.Classes;

namespace YoutubeSubscriptions
{
	public partial class FSubEditor : Form
	{
		private static List<string> EditButtonCaptions = new List<string>(new string[3] { "Add new subscription", "Delete currently selected", "Save currently selected" });
		private static List<string> MoveButtonCaptions = new List<string>(new string[2] { "Move up", "Move down" });
		private static List<string> ExtrasButtonCaptions = new List<string>(new string[1] { "Clear local data" });
		private static List<string> FormControlButtonCaptions = new List<string>(new string[1] { "CLOSE EDITOR" });

		private List<PictureBoxButton> EditButtons, MoveButtons, ExtrasButtons, FormControlButtons;
		private FMain MainForm;

		//private 

		public FSubEditor(FMain MainForm)
		{
			InitializeComponent();
			this.MainForm = MainForm;
			MyGUIs.InitializeAndFormatFormComponents(this);
		}

		private void FSubEditor_Load(object sender, EventArgs e)
		{
			EditButtons = MyGUIs.CreateMenuButtons(EditingP, EditButtonCaptions, false, EditButton_Click);
			MoveButtons = MyGUIs.CreateMenuButtons(MoveP, MoveButtonCaptions, true, MoveButton_Click);
			ExtrasButtons = MyGUIs.CreateMenuButtons(ExtrasP, ExtrasButtonCaptions, false, ExtrasButton_Click);
			FormControlButtons = MyGUIs.CreateMenuButtons(FormControlP, FormControlButtonCaptions, true, FormControlButton_Click);
			RefreshSubs();
		}

		private void RefreshSubs()
		{
			SubsTB.Items.Clear();
			foreach (TYoutuber y in MainForm.Database.Youtubers)
				SubsTB.Items.Add(y.ID);
			SubsTB.SelectedIndex = SubsTB.Items.Count > 0 ? 0 : -1;
		}

		private void SubsTB_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SubsTB.SelectedIndex == -1)
			{
				YoutuberIdTB.Text = "";
				YoutuberNameTB.Text = "";
			}
			else
			{
				TYoutuber youtuber = MainForm.Database.GetYoutuberByID((string) SubsTB.Items[SubsTB.SelectedIndex]);
				if (youtuber == null)
				{
					MessageBox.Show("Could not find the youtuber with the ID \"" + (string) SubsTB.Items[SubsTB.SelectedIndex] + "\" !", "Save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					RefreshSubs();
					return;
				}
				YoutuberIdTB.Text = youtuber.ID;
				YoutuberNameTB.Text = youtuber.Name;
			}
		}

		private void MoveButton_Click(object sender, EventArgs e)
		{
			int r = MoveButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
			int selIndex = SubsTB.SelectedIndex;
			if (r < 0 && r > 1 || r == 0 && selIndex <= 0 || r == 1 && selIndex >= SubsTB.Items.Count - 1)
				return;
			TYoutuber auxY = MainForm.Database.Youtubers[selIndex];
			switch (r)
			{
				case 0: // move up
					MainForm.Database.Youtubers[selIndex] = MainForm.Database.Youtubers[selIndex - 1];
					MainForm.Database.Youtubers[selIndex - 1] = auxY;
					break;
				case 1: // move down
					MainForm.Database.Youtubers[selIndex] = MainForm.Database.Youtubers[selIndex + 1];
					MainForm.Database.Youtubers[selIndex + 1] = auxY;
					break;
			}
			RefreshSubs();
			SubsTB.SelectedIndex = r == 0 ? selIndex - 1 : selIndex + 1;
		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			PictureBoxButton pbb = sender as PictureBoxButton;
			int r = EditButtonCaptions.IndexOf(pbb.Caption);
			switch (r)
			{
				case 0: // add new
					TYoutuber youtuber = new TYoutuber();
					youtuber.ID = "newYoutuberID";
					youtuber.Name = "[new Youtuber]";
					youtuber.InfoLastUpdated = DateTime.Now;
					youtuber.Joined = new DateTime(2000, 1, 1);
					youtuber.Subscribers = 0;
					youtuber.TotalVideos = 0;
					youtuber.TotalViews = 0;
					youtuber.Videos = new List<TVideo>();
					MainForm.Database.Youtubers.Add(youtuber);
					RefreshSubs();
					SubsTB.SelectedIndex = SubsTB.Items.IndexOf(youtuber.ID);
					YoutuberIdTB.Focus();
					break;
				case 1: // delete current
					if (MainForm.Database.Youtubers.Count == 1)
					{
						MessageBox.Show("Please don't delete the last subscription. Edit it.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
					}
					int selIndex = SubsTB.SelectedIndex;
					youtuber = MainForm.Database.GetYoutuberByID((string) SubsTB.Items[selIndex]);
					if (MessageBox.Show("Are you sure you want to delete the subscription to \"" + youtuber.ID + "\" ?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						if (!MainForm.Database.Youtubers.Remove(youtuber))
							MessageBox.Show("Could not delete the current subscription :( !", "Delete ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					RefreshSubs();
					SubsTB.SelectedIndex = selIndex < SubsTB.Items.Count ? selIndex : SubsTB.Items.Count - 1;
					break;
				case 2: // save current
					string yID = (string) SubsTB.Items[SubsTB.SelectedIndex];
					for (int i = 0; i < SubsTB.Items.Count; i++)
						if (i != SubsTB.SelectedIndex && yID.ToLower().Equals(((string) SubsTB.Items[i]).ToLower()))
						{
							MessageBox.Show("This youtuber already exists in the database!", "Duplicate entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
					youtuber = MainForm.Database.GetYoutuberByID(yID);
					youtuber.ID = YoutuberIdTB.Text;
					youtuber.Name = YoutuberNameTB.Text;
					SubsTB.Items[SubsTB.SelectedIndex] = youtuber.ID;
					pbb.Caption = "Saved!";
					PictureBoxButton.OnMouseEnter(pbb, null);
					pbb.Caption = EditButtonCaptions[2];
					break;
				case 3: // something else
					MessageBox.Show("Not implemented yet, WTF?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
			}
		}

		private void ExtrasButton_Click(object sender, EventArgs e)
		{
			int r = ExtrasButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
			switch (r)
			{
				case 0: // clear local thumbnails
					if (MessageBox.Show("Are you sure you want to delete all the downloaded video and youtuber thumbnails, as well as the cached data files?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						try
						{
							MainForm.VideoForm.Hide();
							//
							List<string> files = new List<string>(Directory.GetFiles(Paths.ThumbnailFolder, "*.jpg")); 
                            files.AddRange(Directory.GetFiles(Paths.GDataXmlFolder, "*.xml"));
                            List<string> failed = new List<string>();
							foreach (string file in files)
								try { File.Delete(file); }
								catch (Exception E) { failed.Add("* " + file + ": " + E.Message); }
							//
							MainForm.SubManager.RecreateSubscriptionBoxes();
							MainForm.VideoManager.RecreateVideoBoxes(MainForm.Settings);
							//
							if (failed.Count == 0)
								MessageBox.Show("Successfully deleted " + Utils.RegularPlural("file", files.Count, true) + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
							else
							{
								StringBuilder sb = new StringBuilder("Deletion failed! Of the total ");
								sb.Append(Utils.RegularPlural("file", files.Count, true)).Append(" to be deleted, the following ").Append(failed.Count).Append(" failed:\n\n");
								foreach (string file in failed)
									sb.Append(file).Append("\n\n");
								MessageBox.Show(sb.ToString(), "Deletion failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						catch (Exception E)
						{ MessageBox.Show(E.ToString(), "ExtrasButton_Click() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
					break;
				case 1: // something else
					MessageBox.Show("Not implemented yet, WTF?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
			}
		}

		private void FormControlButton_Click(object sender, EventArgs e)
		{
			int r = FormControlButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
			switch (r)
			{
				case 0: // save and exit
					string saveResult = TDatabase.SaveDatabase(MainForm.Database);
					if (!saveResult.Equals(""))
						MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.MainForm.ShowAndFocusFormAndHideTheRest(null);
					if (MainForm.CurrentMode == FMain.AppModes.Subs)
						MainForm.SubManager.RecreateSubscriptionBoxes();
					MainForm.VideoManager.RecreateVideoBoxes(MainForm.Settings);
					MainForm.Focus();
					break;
				case 1: // something else
					MessageBox.Show("Not implemented yet, WTF?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
			}
		}

		private void YoutuberIdTB_TextChanged(object sender, EventArgs e)
		{
			if (YoutuberIdTB.Text.Length <= 1 ||
				YoutuberNameTB.Text.Equals("[new Youtuber]") ||
				YoutuberNameTB.Text.Equals(YoutuberIdTB.Text.Substring(0, YoutuberIdTB.Text.Length - 1)) /* ||
				YoutuberNameTB.Text.Substring(YoutuberNameTB.Text.Length - 1).Equals(YoutuberIdTB.Text)*/)
				YoutuberNameTB.Text = YoutuberIdTB.Text;
		}
	}
}
