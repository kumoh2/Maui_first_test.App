using Oracle.ManagedDataAccess.Client;

namespace Maui_first_test;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void Login_conn()
	{
        string ConString = "Data Source=172.16.66.49;User ID=scott;Password=tiger";
        string CmdString = string.Format("SELECT PWD FROM Z_USR_MAST_REC" + " "
                                        + "WHERE USR_ID = '{0}'", id_textbox.Text);
        using (OracleConnection conn = new OracleConnection(ConString))
        {
            OracleCommand cmd = new OracleCommand(CmdString, conn);
            conn.Open();

            if (id_textbox.Text == "" || pw_textbox.Text == "")
            {
                DisplayAlert("Alert", "ID 또는Password를입력하세요...", "OK");
                return;
            }

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (pw_textbox.Text != reader["pwd"].ToString())
                    {
                        DisplayAlert("Alert", "Password가맞지않습니다...", "OK");
                        return;
                    }
                }
                else
                {
                    DisplayAlert("Alert", "등록되지않은ID 입니다.", "OK");
                    return;
                }            
            }

            DisplayAlert("Alert", "로그인 성공", "OK");
        }
    }

	private void Login_Button_Click(object sender, EventArgs e)
	{
        Login_conn();
    }
	private void Exit_Button_Click(object sender, EventArgs e)
	{
		Environment.Exit(0);
	}
}