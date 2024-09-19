import { Link, useNavigate } from "react-router-dom";
import "./Navbar.css";
import LogoutIcon from "@mui/icons-material/Logout";
import UpdatePassword from "../Modal/UpdatePassword/UpdatePassword";
import { useCallback, useEffect, useState } from "react";
import logo from "/assets/logo.png";

function Navbar({ role }) {
  const [isUpdatePasswordOpen, setIsUpdatePasswordOpen] = useState(false);
  const [nav, setNav] = useState([]);
  const navigate = useNavigate();

  const handleClose = () => {
    setIsUpdatePasswordOpen(false);
  };

  const handleOpen = () => {
    setIsUpdatePasswordOpen(true);
  };

  const getNavOption = useCallback((role) => {
    let nav; // Declare nav inside the function
    switch (role) {
      case "admin":
        nav = [
          { title: "User", navLink: "user" },
          { title: "User Activities", navLink: "activity" },
          { title: "Config", navLink: "config" },
        ]; // Corrected 'user Actovoties' to 'user Activities'
        break;
      case "manager":
        nav = [
          { title: "Logs", navLink: "/log" },
          { title: "consolidate", navLink: "/consolidate" },
          { title: "Detail", navLink: "/detail" },
        ];
        break;
      default:
        nav = [""];
    }
    return nav; // Return the nav array
  }, []);

  useEffect(() => {
    const nav = getNavOption(role);
    setNav(nav);
  }, [role]);

  const handleLogout = () => {
    sessionStorage.clear();
    navigate("/login");
  };

  return (
    <div className="nav-container">
      <div className="nav-title">
        <img src={logo} height="40px" />
      </div>
      <ul className="nav-items">
        {nav?.map((n, index) => (
          <li key={index}>
            <Link to={n.navLink}> {n.title}</Link>
          </li>
        ))}
        <li className="nav-items" onClick={handleOpen}>
          UpdatePassword
        </li>
        <li className="nav-items" onClick={handleLogout}>
          <LogoutIcon />
        </li>
      </ul>
      <UpdatePassword open={isUpdatePasswordOpen} handleClose={handleClose} />
    </div>
  );
}

export default Navbar;
