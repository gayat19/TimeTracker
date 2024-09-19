import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import "./DeleteActivityModal.css";
import { deleteActivity } from "../../../Service/activity.service";
import { toast } from "react-toastify";
import theme from "../../../utils/theme";
import { useState } from "react";

function DeleteActivityModal({ open, handleClose, id }) {
  const [success, setSuccess] = useState(true);

  const handleDelete = () => {
    setSuccess(true);
    deleteActivity(id)
      .then((res) => {
        toast.success("Activity Deleted Sucessfully");
        handleClose();
        setSuccess(true);
      })
      .catch((err) => {
        setSuccess(true);
      });
  };

  const handleCancel = () => {
    handleClose();
  };

  return (
    <div className="delete-activity-modal">
      <Dialog
        open={open}
        onClose={(_, reason) => {
          reason === "backdropClick" && open;
        }}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle sx={{ color: theme.palette.common.secondaryColor }}>
          <b>Delete Activity</b>
        </DialogTitle>
        <DialogContent>
          <DialogContentText>
            Are your to delete the <b>{`Activity ID : ${id}`}</b> Activity?
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
            type="submit"
            variant="contained"
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

export default DeleteActivityModal;
