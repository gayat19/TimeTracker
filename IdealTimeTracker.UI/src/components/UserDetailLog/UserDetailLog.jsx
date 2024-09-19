import {
  Button,
  Input,
  MenuItem,
  Select,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
} from "@mui/material";
import "./UserDetailLog.css";
import Paper from "@mui/material/Paper";
import { useEffect, useState } from "react";
import { getDetailLogForUser } from "../../Service/log.service";
import moment from "moment";
import { exportDataToExcel } from "../../utils/excel";
import theme from "../../utils/theme";
import { excelTypeAll, excelTypeConsolidated } from "../../utils/contants";
import { getAllUserEmployeeId } from "../../Service/user.service";

function UserDetailLog() {
  const [now, setNow] = useState(moment());

  const tableColums = [
    "S.NO",
    "EMPID",
    "NAME",
    "Duration",
    "Activity",
    "Reason",
    "Activity At ",
    "Date",
  ];

  const [empId, setEmpId] = useState("");
  const [date, setDate] = useState(now.format("YYYY-MM-DD"));
  const [empIds, setEmpIds] = useState([]);
  const [logs, setLogs] = useState([]);

  // const [currentDate, setCurrentDate] = useState(now);

  const handleChange = (event) => {
    setEmpId(event.target.value);
  };

  const [managerId, setmanagerId] = useState(sessionStorage.getItem("userID"));

  useEffect(() => {
    getAllUserEmployeeId(managerId).then((res) => {
      setEmpIds(res.data);
      setEmpId(res.data[0].empId);
      const data = { empId: res.data[0].empId, date: date };
      getDetailLogForUser(data).then((response) => {
        const updatedData = response.data.map((emp) => ({
          ...emp,
          name: response.data[0].name ?? "",
          reasons: emp.reasons === null ? "" : emp.reasons, // Assuming you want to use the name from the first element
        }));
        setLogs(updatedData);
      });
    });
  }, [managerId]);

  useEffect(() => {
    const data = { empId: empId, date: date };
    getDetailLogForUser(data).then((res) => {
      const updatedData = res.data.map((emp) => ({
        ...emp,
        name: empIds.find((emp) => emp.empId === empId)?.name,
        reasons: emp.reasons === null ? "" : emp.reasons,
      }));

      setLogs(updatedData);
    });
  }, [date, empId]);

  const ITEM_HEIGHT = 458;
  const ITEM_PADDING_TOP = 8;
  const MenuProps = {
    PaperProps: {
      style: {
        maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        width: 250,
      },
    },
  };

  const handleDownload = () => {
    // const columns = [
    //   { header: "S.NO", key: "sno", width: 32 },
    //   { header: "EMPID", key: "empId", width: 32 },
    //   // { header: "NAME", key: "name", width: 32 },
    //   { header: "Activity", key: "activity", width: 32 },
    //   // { header: "Reason", key: "reason", width: 20 },
    //   { header: "Activity At ", key: "activityAt", width: 20 },
    //   { header: "Date", key: "date", width: 32 },
    //   { header: "Duration", key: "duration", width: 32 },
    //   // Adjust width as needed
    // ];

    const columns = [
      { header: "S no", key: "sno", width: 32 },

      { header: "EMPID", key: "empId", width: 32 },
      { header: "Name", key: "name", width: 32 },
      { header: "Duration", key: "duration", width: 20 },
      { header: "Activity", key: "activity", width: 32 },
      { header: "Date", key: "date", width: 20 },
      { header: "Activity At ", key: "activityAt", width: 20 },
      { header: "Reason ", key: "reasons", width: 20 },
      // Adjust width as needed
    ];

    exportDataToExcel(
      columns,
      logs,
      excelTypeConsolidated,
      `EMP${empId}-${date.toString()}-detailed-report`
    );
  };

  return (
    <div className="log-detail-container">
      <div className="log-detail-header">
        <div className="log-detail-title">
          <h1> log detail </h1>
        </div>
        <div className="log-detail-date-container">
          <Select
            labelId="dropdown-label"
            value={empId}
            onChange={handleChange}
            MenuProps={MenuProps}
            label="Select an Employee"
            sx={{ width: "200px", height: "50px" }}
          >
            {empIds.map((empid, index) => (
              <MenuItem key={index} value={empid.empId}>
                EMP {empid.empId} : {empid.name}
              </MenuItem>
            ))}
          </Select>

          <Input
            label="Date"
            type="date"
            value={date}
            variant="contained"
            onChange={(e) => setDate(e.target.value)}
          />

          <Button
            variant="contained"
            sx={{
              backgroundColor: theme.palette.common.secondaryColor,
              color: theme.palette.common.primaryColor,
            }}
            disabled={logs.length == 0}
            onClick={handleDownload}
          >
            Download Excel
          </Button>
        </div>
      </div>
      <div className="log-detail-content">
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
              {logs.length > 0 ? (
                logs.map((Activity, index) => (
                  <TableRow key={index}>
                    <TableCell>{index + 1}</TableCell>
                    <TableCell>{Activity.empId}</TableCell>
                    <TableCell>
                      {empIds.find((emp) => emp.empId === empId)?.name}
                    </TableCell>

                    <TableCell>{Activity.duration}</TableCell>
                    <TableCell>{Activity.activity}</TableCell>
                    <TableCell>
                      {Activity.reasons === "" ? "-" : Activity.reasons}
                    </TableCell>
                    <TableCell>
                      {moment(new Date(Activity.activityAt)).format(
                        "DD-MM-YYYY HH:mm:ss"
                      )}
                    </TableCell>
                    <TableCell>
                      {moment(new Date(Activity.date)).format("DD-MM-YYYY")}
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
      </div>
    </div>
  );
}

export default UserDetailLog;
