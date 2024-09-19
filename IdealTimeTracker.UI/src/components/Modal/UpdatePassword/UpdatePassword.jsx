import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
} from "@mui/material";
import "./UpdatePassword.css";
import { toast } from "react-toastify";
import { useState } from "react";
import { updatePassword } from "../../../Service/user.service";
import theme from "../../../utils/theme";

function UpdatePassword({ open, handleClose }) {
  const [password, setPassword] = useState("");
  const [touched, setTouched] = useState(false);

  const handleCancel = () => {
    handleClose();
  };

  const handleDelete = () => {
    const userId = sessionStorage.getItem("userID");
    updatePassword({ EmpId: userId, newPassword: password })
      .then((res) => {
        toast.success("Password updated succesfully");
        handleClose();
      })
      .catch((err) => {
        toast.error(err.response.data.errorMessage);
      });
  };

  const handleChange = (e) => {
    setPassword(e.target.value);
    setTouched(true);
  };

  return (
    <div className="update-password-modal">
      <Dialog
        open={open}
        onClose={(_, reason) => {
          reason === "backdropClick" && open;
        }}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle sx={{ color: theme.palette.common.secondaryColor }}>
          <b>Update Password</b>
        </DialogTitle>
        <DialogContent>
          <TextField
            className="add-user-modal-items"
            label="New Password"
            error={touched && password.length < 8}
            helperText={
              touched && password.length < 8
                ? "Password must be 8 char long"
                : ""
            }
            fullWidth
            margin="normal"
            id="outlined-error"
            onChange={handleChange}
            autoFocus
          />
        </DialogContent>
        <DialogActions>
          <Button
            type="button"
            sx={{
              color: theme.palette.common.secondaryColor,
            }}
            onClick={handleCancel}
          >
            Cancel
          </Button>
          <Button
            type="submit"
            sx={{
              backgroundColor: theme.palette.common.secondaryColor,
              color: theme.palette.common.primaryColor,
            }}
            onClick={handleDelete}
            variant="contained"
          >
            Update
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default UpdatePassword;
