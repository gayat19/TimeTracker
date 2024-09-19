import {
  Button,
  Dialog,
  DialogActions,
  DialogTitle,
  TextField,
} from "@mui/material";
import "./AddorEditActivityModal.css";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { addActivity, editActivity } from "../../../Service/activity.service";
import { toast } from "react-toastify";
import theme from "../../../utils/theme";

function AddorEditActivityModal({ handleClose, open, activity }) {
  // Form
  const [success, setSuccess] = useState(true);

  const { register, handleSubmit, formState, reset } = useForm({
    defaultValues: {
      activity: "",
      countPerDay: 0,
      durationInMins: 0,
    },
  });
  const { errors } = formState;

  //  UseEffect for edit
  useEffect(() => {
    if (activity) {
      reset({
        activity: activity.activity,
        countPerDay: activity.countPerDay,
        durationInMins: activity.durationInMins,
      });
    } else {
      reset({
        activity: "",
        countPerDay: 0,
        durationInMins: 0,
      });
    }
  }, [activity, reset]);

  const onSubmit = (data) => {
    setSuccess(false);
    if (activity === null) {
      addActivity(data)
        .then((res) => {
          toast.success("Activity Added Successfully");
          handleReset();
          setSuccess(true);
        })
        .catch((err) => {
          setSuccess(true);
        });
    } else {
      data.id = activity.id;
      editActivity(data)
        .then((res) => {
          toast.success("Activity Updated Successfully");
          handleReset();
          setSuccess(true);
        })
        .catch((err) => {
          setSuccess(true);
        });
    }
  };

  const handleReset = () => {
    reset({
      activity: "",
      countPerDay: 0,
      durationInMins: 0,
    });
    handleClose();
  };

  return (
    <div className="add-activity-modal">
      <Dialog
        open={open}
        onClose={(_, reason) => {
          reason === "backdropClick" && open;
        }}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle sx={{ color: theme.palette.common.secondaryColor }}>
          {activity === null ? <b>Add Activity</b> : <b>Edit Activity</b>}
        </DialogTitle>
        <form className="add-activity-form" onSubmit={handleSubmit(onSubmit)}>
          <TextField
            required
            className="add-activity-items"
            error={!!errors.activity}
            id="outlined-error"
            label="Name"
            {...register("activity", {
              required: "name is required",
            })}
            helperText={errors.activity?.message}
            disabled={activity !== null}
          />
          <TextField
            required
            className="add-activity-items"
            label="Duration In Minutes"
            {...register("durationInMins", {
              required: "durationInMins is required",
              min: { value: 0, message: "Minimum value is 0" },
            })}
            error={!!errors.durationInMins}
            helperText={errors.durationInMins?.message}
            fullWidth
            type="number"
            margin="normal"
            id="outlined-error"
          />

          <TextField
            required
            className="add-activity-items"
            label="Count Per Day"
            {...register("countPerDay", {
              required: "CountPerDay is required",
              min: { value: 1, message: "Minimum value is 1" },
            })}
            error={!!errors.countPerDay}
            helperText={errors.countPerDay?.message}
            fullWidth
            type="number"
            margin="normal"
            id="outlined-error"
          />

          <DialogActions>
            <Button
              type="reset"
              sx={{
                color: theme.palette.common.secondaryColor,
              }}
              onClick={handleReset}
            >
              Cancel
            </Button>
            <Button
              type="submit"
              variant="contained"
              sx={{
                backgroundColor: theme.palette.common.secondaryColor,
                color: theme.palette.common.primaryColor,
              }}
              autoFocus
              disabled={!success}
            >
              {activity === null ? "Add" : "Edit"}
            </Button>
          </DialogActions>
        </form>
      </Dialog>
    </div>
  );
}

export default AddorEditActivityModal;
