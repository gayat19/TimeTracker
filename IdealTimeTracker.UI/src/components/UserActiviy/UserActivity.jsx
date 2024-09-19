import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import "./UserActivity.css";
import Paper from "@mui/material/Paper";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useEffect, useState } from "react";
import AddorEditActivityModal from "../Modal/AddorEditActivity/AddorEditActivityModal";
import DeleteActivityModal from "../Modal/DeleteActivity/DeleteActivityModal";
import { getAllActivity } from "../../Service/activity.service";
import { toast } from "react-toastify";
import theme from "../../utils/theme";

function UserActivity() {
  const [open, setOpen] = useState(false);
  const [deleteModal, setDeleteModal] = useState(false);
  const [activities, setActivities] = useState([]);
  const [deleteId, setDeleteId] = useState(0);

  const tableColums = [
    "Name",
    "Count per Day",
    "Duration (mins)",
    "Edit",
    "Delete",
  ];

  useEffect(() => {
    getAllActivity()
      .then((res) => setActivities(res.data))
      .catch((err) => toast.error(err.response.data.errorMessage));
  }, [open, deleteModal]);

  const [editActivity, setEditActivity] = useState({
    id: 0,
    activity: "",
    countPerDay: 0,
    durationInMins: 0,
  });

  const handleEdit = (index) => {
    const activity = activities[index];
    if (
      activity.id === 1 ||
      activity.id === 2 ||
      activity.id === 3 ||
      activity.id == 4
    ) {
      return;
    }
    setEditActivity((prev) => ({
      ...prev,
      id: Number(activity.id),
      activity: activity.activity,
      countPerDay: activity.countPerDay,
      durationInMins: activity.durationInMins,
    }));
    handleOpen(true);
  };

  const handleAdd = () => {
    setEditActivity(null);
    handleOpen(true);
  };

  const handleDelete = (index) => {
    setDeleteId(activities[index].id);
    setDeleteModal(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleDeleteClose = () => {
    setDeleteModal(false);
  };

  const handleOpen = () => {
    setOpen(true);
  };
  return (
    <div className="activity-container">
      <div className="activity-header">
        <div className="activity-title">
          <h1>Activity </h1>
        </div>
        <div className="activity-btn">
          <Button
            variant="contained"
            sx={{
              backgroundColor: theme.palette.common.secondaryColor,
              color: theme.palette.common.primaryColor,
            }}
            onClick={handleAdd}
          >
            Add Activity
          </Button>
        </div>
      </div>
      <div className="activity-content">
        <TableContainer
          component={Paper}
          sx={{
            maxHeight: 500,
            "&::-webkit-scrollbar": { width: 1 },
            "&::-webkit-scrollbar-thumb": { backgroundColor: "#888" },
            "&::-webkit-scrollbar-thumb:hover": { backgroundColor: "#555" },
          }}
        >
          <Table stickyHeader>
            <TableHead>
              <TableRow>
                {tableColums.map((col, index) => (
                  <TableCell
                    sx={{
                      backgroundColor: theme.palette.common.secondaryColor,
                      color: theme.palette.common.primaryColor,
                      fontWeight: "bold",
                    }}
                    key={index}
                  >
                    {col}
                  </TableCell>
                ))}
              </TableRow>
            </TableHead>
            <TableBody>
              {activities
                ?.sort((a, b) => b.id - a.id)
                .map((Activity, index) => (
                  <TableRow key={index}>
                    <TableCell>{Activity.activity}</TableCell>
                    <TableCell>{Activity.countPerDay}</TableCell>
                    <TableCell>{Activity.durationInMins}</TableCell>

                    <TableCell
                      className={
                        activities[index].id == 1 ||
                        activities[index].id == 2 ||
                        activities[index].id == 3 ||
                        activities[index].id == 4
                          ? "table-action-disabled"
                          : "table-action"
                      }
                      onClick={() =>
                        !(
                          activities[index].id == 1 ||
                          activities[index].id == 2 ||
                          activities[index].id == 3 ||
                          activities[index].id == 4
                        ) && handleEdit(index)
                      }
                    >
                      <EditIcon />
                    </TableCell>
                    <TableCell
                      className={
                        activities[index].id == 1 ||
                        activities[index].id == 2 ||
                        activities[index].id == 3 ||
                        activities[index].id == 4
                          ? "table-action-disabled"
                          : "table-action"
                      }
                      onClick={() =>
                        !(
                          activities[index].id == 1 ||
                          activities[index].id == 2 ||
                          activities[index].id == 3 ||
                          activities[index].id == 4
                        ) && handleDelete(index)
                      }
                    >
                      <DeleteIcon />
                    </TableCell>
                  </TableRow>
                ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>

      <AddorEditActivityModal
        open={open}
        handleClose={handleClose}
        activity={editActivity}
      />

      <DeleteActivityModal
        id={deleteId}
        open={deleteModal}
        handleClose={handleDeleteClose}
      />
    </div>
  );
}

export default UserActivity;
