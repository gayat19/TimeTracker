import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import "./User.css";
import Paper from "@mui/material/Paper";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useEffect, useState } from "react";
import AddUserModal from "../Modal/AddorEditUser/AddUserModal";
import DeleteUserModal from "../Modal/DeleteUserModal/DeleteUserModal";
import { getAllUser } from "../../Service/user.service";
import { toast } from "react-toastify";
import theme from "../../utils/theme";
import Pagination from "@mui/material/Pagination";
import Stack from "@mui/material/Stack";

function User() {
  const [open, setOpen] = useState(false);
  const [deleteModal, setDeleteModal] = useState(false);
  const [employees, setEmployees] = useState([]);
  const [deleteId, setDeleteId] = useState(null);

  const tableColums = [
    "Employee Id",
    "Name",
    "Email",
    "ReportingTo",
    "Role",
    "Edit",
    "Delete",
  ];

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPage, setTotalPage] = useState(1);

  const handlePagination = (event, value) => {
    setCurrentPage(value);
  };

  useEffect(() => {
    getAllUser(currentPage)
      .then((res) => {
        const { users, totalPages } = res.data;
        setTotalPage(totalPages);
        setEmployees(users);
      })
      .catch((err) => {
        toast.error(err.response.data.errorMessage);
      });
  }, [open, deleteModal, currentPage]);

  const [editEmployee, setEditEmployee] = useState({
    email: "",
    name: "",
    reportingTo: "",
    role: "",
    empId: "",
    password: "",
  });

  const handleEdit = (index) => {
    const emp = employees[index];
    setEditEmployee((prev) => ({
      ...prev,
      email: emp.email,
      name: emp.name,
      reportingTo: emp.reportingTo,
      role: emp.role,
      empId: emp.empId,
      password: emp.passWord,
    }));
    handleOpen(true);
  };

  const handleAdd = () => {
    setEditEmployee(null);
    handleOpen(true);
  };

  const handleDelete = (index) => {
    setDeleteId(employees[index].empId);
    setDeleteModal(true);
  };

  // MOdal
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
    <div className="user-container">
      <div className="user-header">
        <div className="user-title">
          <h1>User </h1>
        </div>
        <div className="user-btn">
          <Button
            variant="contained"
            sx={{
              backgroundColor: theme.palette.common.secondaryColor,
              color: theme.palette.common.primaryColor,
            }}
            onClick={handleAdd}
          >
            Add user
          </Button>
        </div>
      </div>
      <div className="user-content">
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
              {employees.length > 0 ? (
                employees
                  ?.sort((a, b) => b.id - a.id)
                  .map((employee, index) => (
                    <TableRow key={index}>
                      <TableCell>{employee.empId}</TableCell>
                      <TableCell>{employee.name}</TableCell>
                      <TableCell>{employee.email}</TableCell>
                      <TableCell>
                        {employee.reportingTo == null
                          ? "none"
                          : employee.reportingTo}
                      </TableCell>
                      <TableCell>{employee.role}</TableCell>

                      <TableCell
                        className="table-action"
                        onClick={() => handleEdit(index)}
                      >
                        <EditIcon />
                      </TableCell>
                      <TableCell
                        className="table-action"
                        onClick={() =>
                          employee.empId !== "ADMIN" && handleDelete(index)
                        }
                      >
                        <DeleteIcon />
                      </TableCell>
                    </TableRow>
                  ))
              ) : (
                <TableRow>
                  <TableCell colSpan={tableColums.length} align="center">
                    No records
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>
        <div className="user-pagination">
          <Pagination
            page={currentPage}
            count={totalPage}
            onChange={handlePagination}
            color="primary"
          />
        </div>
      </div>
      <AddUserModal
        open={open}
        handleClose={handleClose}
        employee={editEmployee}
      />

      <DeleteUserModal
        id={deleteId}
        open={deleteModal}
        handleClose={handleDeleteClose}
      />
    </div>
  );
}

export default User;
