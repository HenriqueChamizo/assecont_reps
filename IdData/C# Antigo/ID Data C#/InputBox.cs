using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IdData
{
    class InputBox
    {
         private static Form frmInputBox = new Form();  
         private static Label lblPrompt = new Label();  
         private static Button btnOk = new Button();  
         private static Button btnCancel = new Button();  
         private static TextBox txtResponse = new TextBox();  
           
   
         /// <summary>  
         /// Initialize the controls  
         /// </summary>  
         static InputBox()  
         {  
         
             frmInputBox.StartPosition = FormStartPosition.CenterScreen;  
             frmInputBox.MinimizeBox = false;  
             frmInputBox.MaximizeBox = false;  
             frmInputBox.FormBorderStyle = FormBorderStyle.FixedDialog;

             frmInputBox.Controls.AddRange(new Control[] { lblPrompt , btnOk , btnCancel, txtResponse});  
             frmInputBox.Size = new Size(346, 180);  
             frmInputBox.AcceptButton = btnOk;  
             frmInputBox.CancelButton = btnCancel;   
   
             btnOk.Size = new Size(70, 23);  
             btnOk.Location = new Point(180, 121);
   
             btnCancel.Size = new Size(70, 23);  
             btnCancel.Location = new Point(255, 121);
   
             lblPrompt.AutoSize = true;  
             lblPrompt.Location = new Point(12, 37);
             Font font = new Font("Tahoma", 12);
             lblPrompt.Font = font;
             lblPrompt.ForeColor = Color.Black;
   
             txtResponse.Size = new Size(300, 20);  
             txtResponse.Location = new Point(12, 75);
         }  
   
         public static DialogResult Show (string Prompt, string title, ref string value )  
         {  
             frmInputBox.Text = title;  
             lblPrompt.Text = Prompt;  
             btnOk.Text = "&OK";  
             btnCancel.Text = "&Cancelar";
             btnOk.DialogResult = DialogResult.OK;  
             btnCancel.DialogResult = DialogResult.Cancel;
             txtResponse.Text = value;
             txtResponse.Focus();  
   
             DialogResult result = frmInputBox.ShowDialog();  
             value = txtResponse.Text;  
   
             return result;     
         }  
    }  
}