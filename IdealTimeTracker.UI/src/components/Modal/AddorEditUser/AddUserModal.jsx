import {
  Button,
  Dialog,
  DialogActions,
  DialogTitle,
  FormControl,
  FormHelperText,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import "./AddUserModal.css";
import { Controller, useForm, useWatch } from "react-hook-form";
import { useEffect, useState } from "react";
import {
  addUser,
  editUser,
  getReportingDropdown,
} from "../../../Service/user.service";
import { toast } from "react-toastify";
import theme from "../../../utils/theme";

function AddUserModal({ handleClose, open, employee }) {
  // Form
  const { register, handleSubmit, formState, reset, control } = useForm({
    defaultValues: {
      email: null,
      name: "",
      reportingTo: -1,
      role: "employee",
      password: "",
      empId: "",
    },
  });
  const { errors } = formState;

  const [reportingTo, setReportingTo] = useState([]);
  const [success, setSuccess] = useState(true);

  //  UseEffect for edit
  useEffect(() => {
    if (employee) {
      reset({
        email: employee.email,
        name: employee.name,
        reportingTo: employee.reportingTo ?? -1,
        role: employee.role,
        password: employee.password,
        empId: employee.empId,
      });
    } else {
      reset({
        email: null,
        name: "",
        reportingTo: -1,
        role: "employee",
        password: "",
        empId: "",
      });
    }
  }, [employee, reset]);

  useEffect(() => {
    getReportingDropdown()
      .then((res) => {
        setReportingTo(res.data);
      })
      .catch((err) => {
        toast.error(err.response.data.errorMessage);
      });
  }, [open]);

  const roles = ["admin", "manager", "employee"];

  const role = useWatch({
    control,
    name: "role",
  });

  const onSubmit = (data) => {
    if (data.reportingTo === -1) data.reportingTo = null;
    setSuccess(false);
    if (employee === null) {
      addUser(data)
        .then((res) => {
          toast.success("User Sucessfully added");
          handleReset();
          setSuccess(true);
        })
        .catch((err) => {
          toast.error(err.response.data.errorMessage);
          setSuccess(true);
        });
    } else {
      const updateuser = {
        reportingTo: data.reportingTo,
        email: data.email,
        newPassword: data.password,
        empId: employee.empId,
        name: data.name,
      };

      editUser(updateuser)
        .then((res) => {
          toast.success("User Sucessfully Updated");
          handleReset();
          setSuccess(true);
        })
        .catch((err) => {
          toast.error(err.response.data.errorMessage);
          setSuccess(true);
        });
    }
  };

  const handleReset = () => {
    reset({
      empId: "",
      password: "",
      email: "",
      name: "",
      reportingTo: "none",
      role: "employee",
    });
    handleClose();
  };

  return (
    <div className="add-user-modal">
      <Dialog
        open={open}
        onClose={(_, reason) => {
          reason === "backdropClick" && open;
        }}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle sx={{ color: theme.palette.common.secondaryColor }}>
          {employee === null ? <b>Add User</b> : <b>Edit User</b>}
        </DialogTitle>
        <form className="add-user-form" onSubmit={handleSubmit(onSubmit)}>
          <TextField
            required
            className="add-user-modal-items"
            error={!!errors.empId}
            id="outlined-error"
            label="EMP ID"
            fullWidth
            margin="normal"
            {...register("empId", {
              required: "EMPID is required",
            })}
            helperText={errors.empId?.message}
            disabled={employee !== null}
          />

          <TextField
            required
            className="add-user-modal-items"
            error={!!errors.password}
            id="outlined-error"
            label="Password"
            fullWidth
            margin="normal"
            {...register("password", {
              required: "Password is required",
            })}
            helperText={errors.password?.message}
          />

          <TextField
            required={role !== "employee"}
            className="add-user-modal-items"
            error={!!errors.email}
            id="outlined-error"
            fullWidth
            margin="normal"
            label="Email Address"
            {...register("email", {
              required:
                role !== "employee" ? "Email address is required" : false,
              pattern: {
                value: /^\S+@\S+\.\S+$/,
                message: "Invalid email format",
              },
            })}
            helperText={errors.email?.message}
          />

          <TextField
            required
            className="add-user-modal-items"
            label="Name"
            {...register("name", {
              required: "Name is required",
            })}
            error={!!errors.name}
            helperText={errors.name?.message}
            fullWidth
            margin="normal"
            id="outlined-error"
          />

          <FormControl
            fullWidth
            margin="normal"
            required={role == "employee"}
            className="add-user-modal-items"
          >
            <InputLabel id="reportingTo-label">Reporting To</InputLabel>
            <Controller
              name="reportingTo"
              control={control}
              rules={{ required: "Reporting to is required" }}
              render={({ field }) => (
                <Select
                  labelId="reportingTo-label"
                  {...field}
                  label="Reporting To"
                  error={!!errors.reportingTo}
                >
                  <MenuItem key={-1} value={-1}>
                    none
                  </MenuItem>
                  {reportingTo.map((option, index) => (
                    <MenuItem key={index} value={option.empId}>
                      {option.empId} : {option.name}
                    </MenuItem>
                  ))}
                </Select>
              )}
            />
            <FormHelperText error>{errors.reportingTo?.message}</FormHelperText>
          </FormControl>

          <FormControl
            fullWidth
            required
            margin="normal"
            className="add-user-modal-items"
            disabled={employee !== null}
          >
            <InputLabel id="role-label">Role</InputLabel>
            <Controller
              name="role"
              control={control}
              rules={{ required: "Role is required" }}
              render={({ field }) => (
                <Select
                  labelId="role-label"
                  {...field}
                  label="Role"
                  error={!!errors.role}
                >
                  {roles.map((option, index) => (
                    <MenuItem key={index} value={option}>
                      {option}
                    </MenuItem>
                  ))}
                </Select>
              )}
            />
            <FormHelperText error>{errors.role?.message}</FormHelperText>
          </FormControl>

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
              {employee === null ? "Add" : "Edit"}
            </Button>
          </DialogActions>
        </form>
      </Dialog>
    </div>
  );
}

export default AddUserModal;
