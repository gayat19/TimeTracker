import "./UserLogin.css";
import logo from "/assets/logo.png";
function UserLogin() {
  return (
    <div className="UserLogin">
      <img src={logo} height="100px" />
      <p>
        Update your password By clicking <b>"Update Password"</b> In Navbar
      </p>
    </div>
  );
}
export default UserLogin;
