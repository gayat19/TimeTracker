import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import "./DeleteUserModal.css";
import { toast } from "react-toastify";
import { deleteUser } from "../../../Service/user.service";
import theme from "../../../utils/theme";
import { useState } from "react";

function DeleteUserModal({ open, handleClose, id }) {
  const handleCancel = () => {
    handleClose();
  };

  const [success, setSuccess] = useState(true);

  const handleDelete = () => {
    setSuccess(false);
    deleteUser(id)
      .then((res) => {
        toast.success("User Sucessfully deleted");
        handleClose();
        setSuccess(true);
      })
      .catch((err) => {
        toast.error(err.response.data.errorMessage);
        setSuccess(true);
      });
  };

  return (
    <div className="delete-user-modal">
      <Dialog
        open={open}
        onClose={(_, reason) => {
          reason === "backdropClick" && open;
        }}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle sx={{ color: theme.palette.common.secondaryColor }}>
          <b>Delete User</b>
        </DialogTitle>
        <DialogContent>
          <DialogContentText>
            Are your to delete the <b>{`EMP ${id}`} </b>user?
          </DialogContentText>
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
            variant="contained"
            type="submit"
            sx={{
              backgroundColor: theme.palette.common.danger,
              color: theme.palette.common.primaryColor,
            }}
            onClick={handleDelete}
            autoFocus
            disabled={!success}
          >
            Delete
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default DeleteUserModal;
