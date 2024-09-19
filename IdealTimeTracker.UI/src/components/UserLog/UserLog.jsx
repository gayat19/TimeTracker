import {
  Button,
  Pagination,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
} from "@mui/material";
import "./UserLog.css";
import Paper from "@mui/material/Paper";
import { useEffect, useState } from "react";
import { getAllLogs, getAllLogsExcel } from "../../Service/log.service";
import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import moment from "moment";
import { exportDataToExcel } from "../../utils/excel";
import theme from "../../utils/theme";
import { workingHours } from "../../utils/admin.config";
import { excelTypeAll } from "../../utils/contants";
import { toast } from "react-toastify";

function UserLog() {
  const [now, setNow] = useState(moment());

  const tableColums = [
    "S.NO",
    "EMPID",
    "Name",
    "Duration (HH:MM:SS)",
    "Date",
    "Status",
  ];

  const [searchterm, setSearchterm] = useState("");
  const [activities, setActivities] = useState([]);
  // const [currentDate, setCurrentDate] = useState(now);

  const handleChange = (event) => {
    setSearchterm(event.target.value);
  };

  const [userId, seUserId] = useState(sessionStorage.getItem("userID"));

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPage, setTotalPage] = useState(1);

  const handlePagination = (event, value) => {
    setCurrentPage(value);
  };

  const [excelSuccess, setExcelSuccess] = useState(true);

  useEffect(() => {
    if (now) {
      getAllLogs({
        reportingToId: userId,
        start: now.startOf("months").format("YYYY-MM-DD"),
        end: now.endOf("month").format("YYYY-MM-DD"),
        pageNumber: currentPage,
      }).then((res) => {
        setActivities(res.data.logs);
        setTotalPage(res.data.totalPages);
      });
    }
  }, [now, currentPage]);

  const handleDate = (direction) => {
    const updatedDate = now.clone().add(direction, "month"); // Clone and add/subtract month
    setNow(updatedDate);
  };

  const handleDownload = () => {
    const columns = [
      { header: "S no", key: "sno", width: 32 },

      { header: "EMPID", key: "empId", width: 32 },
      { header: "Name", key: "name", width: 32 },
      { header: "Duration", key: "duration", width: 20 },
      { header: "Date", key: "date", width: 20 },
      { header: "Status", key: "status", width: 32 },

      // Adjust width as needed
    ];

    setExcelSuccess(false);
    toast.info("Excel Report started");
    getAllLogsExcel({
      reportingToId: userId,
      start: now.startOf("months").format("YYYY-MM-DD"),
      end: now.endOf("month").format("YYYY-MM-DD"),
    })
      .then(async (res) => {
        setExcelSuccess(true);
        await exportDataToExcel(
          columns,
          res.data,
          excelTypeAll,
          `${now.format("MMMM YYYY").toString()}`
        );
        toast.success("Excel Report Generated");
      })
      .catch(() => {
        toast.error("Excel Report failed");
        setExcelSuccess(true);
      });
  };

  return (
    <div className="log-container">
      <div className="log-header">
        <div className="log-title">
          <h1>log </h1>
        </div>
        <div className="log-date-container">
          <div className="log-date">
            <ArrowBackIosNewIcon
              onClick={() => {
                handleDate(-1);
              }}
              className="log-arrow"
            />
            Month : {now.format("MMMM YYYY")}
            <ArrowForwardIosIcon
              onClick={() => {
                handleDate(1);
              }}
              className="log-arrow"
            />
          </div>
        </div>
        <div className="log-btn">
          <TextField
            className="search-box"
            id="outlined-error"
            label="Search EMP Ids"
            value={searchterm}
            onChange={handleChange}
          />
          <Button
            variant="contained"
            sx={{
              backgroundColor: theme.palette.common.secondaryColor,
              color: theme.palette.common.primaryColor,
            }}
            disabled={!excelSuccess || activities.length == 0}
            onClick={handleDownload}
          >
            Download Excel
          </Button>
        </div>
      </div>
      <div className="log-content">
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
              {activities.length > 0 ? (
                activities
                  ?.filter((acivity) => acivity.empId.includes(searchterm))
                  .map((Activity, index) => (
                    <TableRow key={index}>
                      <TableCell>{index + 1}</TableCell>

                      <TableCell>{Activity.empId}</TableCell>
                      <TableCell>{Activity.name}</TableCell>

                      <TableCell>{Activity.duration}</TableCell>
                      <TableCell>{Activity.date}</TableCell>
                      <TableCell>
                        {Activity.durationInMins > workingHours ? (
                          <span className="log-present">Present</span>
                        ) : (
                          <span className="log-absent">Absent</span>
                        )}
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
        <div className="user-log-pagination">
          <Pagination
            page={currentPage}
            count={totalPage}
            onChange={handlePagination}
            color="primary"
          />
        </div>
      </div>
    </div>
  );
}

export default UserLog;
