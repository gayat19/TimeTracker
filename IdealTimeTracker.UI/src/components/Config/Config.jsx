import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import "./Config.css";
import Paper from "@mui/material/Paper";
import EditIcon from "@mui/icons-material/Edit";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import theme from "../../utils/theme";
import { getAllConfig } from "../../Service/admin.config";
import EditConfig from "../Modal/EditConfig/EditConfig";

function Config() {
  const [open, setOpen] = useState(false);
  const [config, setConfig] = useState(null);
  const [configurations, setConfigurations] = useState([]);

  const tableColums = ["Id", "Name", "Values", "Actions"];

  useEffect(() => {
    getAllConfig()
      .then((res) => {
        const users = res.data;
        setConfigurations(users);
      })
      .catch((err) => {
        toast.error(err.response.data.errorMessage);
      });
  }, [open, config]);

  const handleEdit = (index) => {
    setConfig(configurations[index]);
    setOpen(true);
  };

  const handleChange = (id, value) => {
    setConfigurations((prev) =>
      prev.map((config) => (config.id === id ? { ...config, value } : config))
    );
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div className="config-container">
      <div className="config-header">
        <div className="config-title">
          <h1>Configurations </h1>
        </div>
      </div>
      <div className="config-content">
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
              {configurations.length > 0 ? (
                configurations?.map((config, index) => (
                  <TableRow key={index}>
                    <TableCell>{`${config.id}`}</TableCell>
                    <TableCell>{config.name}</TableCell>
                    <TableCell>
                      {config.value}
                      {config.id === 1 || config.id === 2
                        ? " ( Duration)"
                        : " ( Time in 24 hours )"}
                    </TableCell>
                    <TableCell onClick={() => handleEdit(index)}>
                      <EditIcon />
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

        <EditConfig config={config} handleClose={handleClose} open={open} />
      </div>
    </div>
  );
}

export default Config;
