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
                DisplayAlert("Alert", "ID �Ǵ�Password���Է��ϼ���...", "OK");
                return;
            }

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (pw_textbox.Text != reader["pwd"].ToString())
                    {
                        DisplayAlert("Alert", "Password�������ʽ��ϴ�...", "OK");
                        return;
                    }
                }
                else
                {
                    DisplayAlert("Alert", "��ϵ�������ID �Դϴ�.", "OK");
                    return;
                }            
            }

            DisplayAlert("Alert", "�α��� ����", "OK");
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