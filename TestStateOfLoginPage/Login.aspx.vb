Public Class Login
    Inherits System.Web.UI.Page

    Private loginData As DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loginData = New DataTable()
        loginData.Columns.Add("email")
        loginData.Columns.Add("password")
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles loginButton.Click
        If IsPostBack Then
            loginData.Rows.Add(emailTextBox.Text, passwordTextBox.Text)

            ' Display OTP field
            otpTextBox.Visible = True
        Else

        End If
    End Sub
End Class