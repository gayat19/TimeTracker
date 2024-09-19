import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField,
} from "@mui/material";
import "./EditConfig.css";
import { toast } from "react-toastify";
import theme from "../../../utils/theme";
import { useEffect, useState } from "react";
import { updateConfig } from "../../../Service/admin.config";

function EditConfig({ open, handleClose, config }) {
  const [value, SetValue] = useState();

  const [mins, setMins] = useState();
  const [secs, setSecs] = useState();
  const [hours, setHours] = useState();

  const [success, setSuccess] = useState(true);
  const handleCancel = () => {
    handleClose();
  };

  useEffect(() => {
    if (config) {
      SetValue(config.value);
      const values = config.value.split(":");
      console.log(values);
      setHours(Number(values[0]));
      setMins(Number(values[1]));
      setSecs(Number(values[2]));
    }
  }, [config]);

  const handleEdit = () => {
    const data = {
      id: config?.id,
      value: value,
    };
    setSuccess(false);
    updateConfig(data)
      .then(() => {
        handleClose();
        setSuccess(true);
      })
      .catch((err) => {
        setSuccess(true);
      });
  };

  const handleMins = (e) => {
    const min = Number(e.target.value);
    setMins(min);

    console.log(min, "min");
    const time =
      hours.toString()?.padStart(2, "0") +
      ":" +
      min.toString()?.padStart(2, "0") +
      ":" +
      secs.toString()?.padStart(2, "0");
    SetValue(time);
  };

  const handleSecs = (e) => {
    const sec = Number(e.target.value);
    setSecs(sec);
    console.log(sec, "sec");

    const time =
      hours.toString()?.padStart(2, "0") +
      ":" +
      mins.toString()?.padStart(2, "0") +
      ":" +
      sec.toString()?.padStart(2, "0");
    SetValue(time);
  };

  const handleHours = (e) => {
    const hour = Number(e.target.value);
    setHours(hour);
    console.log(hour, "hours");
    const time =
      hour.toString().padStart(2, "0") +
      ":" +
      mins.toString()?.padStart(2, "0") +
      ":" +
      secs.toString()?.padStart(2, "0");
    SetValue(time);
  };

  return (
    <div className="update-config-modal">
      <Dialog
        open={open}
        onClose={(_, reason) => {
          reason === "backdropClick" && open;
        }}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
        maxWidth="sm"
        fullWidth
        sx={{
          "& .MuiDialog-paper": {
            width: "400px", // Custom width
            maxWidth: "900px", // Set a maximum width if needed
          },
        }}
      >
        <DialogTitle
          sx={{
            color: theme.palette.common.secondaryColor,
            textAlign: "center",
          }}
        >
          <b>Update Configs</b>
        </DialogTitle>
        <DialogContent>
          <DialogContentText sx={{ textAlign: "center" }}>
            Config : <b>{config?.name}</b>{" "}
          </DialogContentText>
          <div className="config-time-input">
            {config?.id === 1 || config?.id === 2 ? (
              <>
                <div className="min-inputs">
                  <div className="min-input">
                    <TextField
                      type="number"
                      value={hours}
                      onChange={handleHours}
                      className="min-input-option"
                      variant="standard"
                      inputProps={{ min: 0, max: 24 }}
                      error={hours < 0 || hours > 24}
                      helperText={
                        hours < 0 || hours > 60
                          ? "Value must be between 0 and 24 hours."
                          : ""
                      }
                    />
                    :
                  </div>

                  {/* <div className="min-input">
                  Hours :
                  <TextField
                    type="number"
                    value={hours}
                    onChange={handleHours}
                    className="min-input-option"
                    variant="standard"
                    inputProps={{ min: 0, hours: 24 }}
                    error={hours < 0 || hours > 24}
                    helperText={
                      hours < 0 || hours > 24
                        ? "Value must be between 0 and 24 hours."
                        : ""
                    }
                  />
                </div> */}

                  <div className="min-input">
                    <TextField
                      required
                      type="number"
                      value={mins}
                      onChange={handleMins}
                      className="min-input-option"
                      variant="standard"
                      inputProps={{ min: 0, max: 60 }}
                      error={mins < 0 || mins > 60}
                      helperText={
                        mins < 0 || mins > 60
                          ? "Value must be between 0 and 60 mins."
                          : ""
                      }
                    />
                    :
                  </div>

                  <div className="min-input">
                    <TextField
                      type="number"
                      value={secs}
                      onChange={handleSecs}
                      variant="standard"
                      className="min-input-option"
                      inputProps={{ min: 0, max: 60 }}
                      error={secs < 0 || secs > 60}
                      helperText={
                        secs < 0 || secs > 60
                          ? "Value must be between 0 and 60 Secs."
                          : ""
                      }
                    />
                  </div>
                </div>
                <p className="slider-values">
                  Time: <b>{value}</b>
                </p>
              </>
            ) : (
              <input
                className="config-time-input-time"
                size={300}
                height={300}
                value={value}
                onChange={(e) => SetValue(e.target.value)}
                type="time"
                step="1"
                locale="zh-CN"
                lang="zh-CN"
              />
            )}
          </div>
        </DialogContent>
        <DialogActions sx={{ display: "flex", justifyContent: "center" }}>
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
              backgroundColor: theme.palette.common.secondaryColor,
              color: theme.palette.common.primaryColor,
            }}
            onClick={handleEdit}
            autoFocus
            disabled={
              !success ||
              ((config?.id === 1 || config?.id === 2) &&
                (mins < 0 || mins > 60)) ||
              secs < 0 ||
              secs > 60 ||
              hours < 0 ||
              hours > 24
            }
          >
            update
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default EditConfig;
