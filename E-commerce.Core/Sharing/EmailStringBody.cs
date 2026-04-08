namespace E_commerce.Core.Sharing
{
	public class EmailStringBody
	{
		public static string Send(string email, string token, string component, string message)
		{
			string encodeToken = Uri.EscapeDataString(token);
			return $@"
					<html>

						<head></head>
						<body>
							<h1>{message}</h1> 
							<hr>
							<br>
							<a href=""http://localhost:4200/account/{component}?email={email}&code={encodeToken}"">{message}</a>
						</ body >


					</ html >
				";
		}
	}
}
