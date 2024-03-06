<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="TestStateOfLoginPage.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <style>
        /* Basic styling */
        .container {
            max-width: 400px;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
    </style>

</head>
<body>
    <form id="loginForm" runat="server">
        <div class="container">
            <h2>Login</h2>
            <asp:TextBox ID="emailTextBox" runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
            <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
            <asp:Panel ID="otp" hidden="true">
                <asp:TextBox ID="otpTextBox" runat="server" CssClass="form-control otp-field" placeholder="Enter OTP"></asp:TextBox>
            </asp:Panel>
            <asp:Button ID="loginButton" runat="server" Text="Login" OnClientClick="return false" OnClick="LoginButton_Click" CssClass="btn btn-primary" />
            
        </div>
    </form>
</body>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
        function handleLoginClick() {
            $.ajax({
                type: "POST",
                url: "Login.aspx/LoginButtonClick",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Display OTP field if required
                    if (response.d === "showOTP") {
                        $("#otpTextBox").show();
                    }
                }
            });
            return false; // Prevent form submission
        }
    </script>
<script>
    var isFirstClick = true;

    function handleLoginButtonClick() {
        if (isFirstClick) {
            isFirstClick = false;
            // Prevent form submission on first click
            return false;
        } else {
            // Allow form submission on subsequent clicks
            return true;
        }
    }
</script>
</html>
