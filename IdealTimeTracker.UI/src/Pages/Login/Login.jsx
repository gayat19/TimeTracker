import { Button, TextField } from "@mui/material";
import "./Login.css";
import { useForm } from "react-hook-form";
import { login } from "../../Service/user.service";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import theme from "../../utils/theme";
import { useEffect } from "react";
import logo from "/assets/logo.png";

function Login() {
  const navigate = useNavigate();
  useEffect(() => {
    if (sessionStorage.getItem("token")) {
      navigate("/");
    }
  }, []);
  const form = useForm({
    defaultValue: {
      email: "",
      password: "",
    },
  });

  const { register, handleSubmit, formState } = form;
  const { errors } = formState;

  const onSubmit = async (data) => {
    login(data)
      .then((res) => {
        const data = res.data;
        sessionStorage.setItem("token", data.token);
        sessionStorage.setItem("role", data.role);
        sessionStorage.setItem("userID", data.empId);
        navigate("/");
      })
      .catch((err) => {
        toast.error(err?.response?.data?.errorMessage);
      });
  };

  return (
    <>
      <div className="login-page">
        <div className="login-container">
          <div className="login-header">
            <img src={logo} height="90px" />
            {/* <h1>Ideal Time Tracker</h1> */}
            <h1 color="red"> Login </h1>
          </div>
          <form
            method="post"
            className="login-form"
            onSubmit={handleSubmit(onSubmit)}
          >
            <TextField
              error={!!errors.email}
              className="login-field"
              id="outlined-error"
              label="Email Address or EMPID"
              {...register("email", {
                required: "Email address is required",
                pattern: {
                  // value: /^\S+@\S+\.\S+$/,
                  message: "Invalid email format",
                },
              })}
              helperText={errors.email?.message}
            />
            <br />
            <TextField
              label="Password"
              type="password"
              error={!!errors.password}
              className="login-field"
              id="outlined-adornment-password"
              {...register("password", {
                required: "Password is required",
                minLength: {
                  value: 2,
                  message: "Password must be at least 8 characters long",
                }, // Example validation
              })}
              helperText={errors.password?.message}
            />

            <br />
            <Button
              sx={{
                backgroundColor: theme.palette.common.secondaryColor,
                color: theme.palette.common.primaryColor,
              }}
              variant="contained"
              type="submit"
            >
              Login
            </Button>
          </form>
        </div>
      </div>
    </>
  );
}

export default Login;
