import {
  Button,
  Input,
  Pagination,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
} from "@mui/material";
import "./UserConsolidate.css";
import Paper from "@mui/material/Paper";
import { useEffect, useState } from "react";
import {
  getCondolidatedLogs,
  getCondolidatedLogsExcel,
} from "../../Service/log.service";
import moment from "moment";
import { exportDataToExcel } from "../../utils/excel";
import theme from "../../utils/theme";
import { workingHours } from "../../utils/admin.config";
import { excelTypeConsolidated } from "../../utils/contants";
import { toast } from "react-toastify";

function UserConsolidate() {
  const tableColums = [
    "S.NO",
    "EMPID",
    "Name",
    "Absent Duration (HH:MM:SS)",
    "Days",
    "Status",
  ];

  const [searchterm, setSearchterm] = useState("");
  const [activities, setActivities] = useState([]);
  const [fromDate, setfromDate] = useState(
    new Date().toISOString().substring(0, 10)
  );
  const [toDate, setToDate] = useState(
    new Date().toISOString().substring(0, 10)
  );

  const handleChange = (event) => {
    setSearchterm(event.target.value);
  };

  const [userId, seUserId] = useState(sessionStorage.getItem("userID"));

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPage, setTotalPage] = useState(1);
  const [excelSuccess, setExcelSuccess] = useState(true);

  const handlePagination = (event, value) => {
    setCurrentPage(value);
  };

  useEffect(() => {
    getCondolidatedLogs({
      reportingToId: userId,
      start: fromDate,
      end: toDate,
      pageNumber: currentPage,
    }).then((res) => {
      setActivities(res.data.logs);
      setTotalPage(res.data.totalPages);
    });
  }, [toDate, fromDate, currentPage]);

  const handleDownload = () => {
    const columns = [
      { header: "S no", key: "sno", width: 32 },

      { header: "EMPID", key: "empId", width: 32 },
      { header: "Name", key: "name", width: 32 },
      {
        header: "Absent Duration (HH:MM:SS)",
        key: "absentDuration",
        width: 20,
      },
      { header: "Days", key: "days", width: 20 },
      { header: "Status", key: "status", width: 32 },

      // Adjust width as needed
    ];

    toast.info("Excel Report Started");
    setExcelSuccess(true);
    getCondolidatedLogsExcel({
      reportingToId: userId,
      start: fromDate,
      end: toDate,
    })
      .then(async (res) => {
        console.log(res);
        await exportDataToExcel(
          columns,
          res.data,
          excelTypeConsolidated,
          `consolidate${fromDate}to${toDate}`
        );
        setExcelSuccess(true);
        toast.success("Excel Report generated");
      })
      .catch(() => {
        setExcelSuccess(true);
        toast.error("Excel Report Failed");
      });
  };

  return (
    <div className="consolidate-container">
      <div className="consolidate-header">
        <div className="consolidate-title">
          <h1>consolidate log </h1>
        </div>
        <div className="consolidate-date-container">
          <div className="consolidate-date">
            <span className="consolidate-date-text">From : </span>
            <Input
              type="date"
              value={fromDate}
              onChange={(e) => setfromDate(e.target.value)}
            />
            <span className="consolidate-date-text">To :</span>

            <Input
              label="To"
              type="date"
              value={toDate}
              onChange={(e) => setToDate(e.target.value)}
            />
          </div>
        </div>
        <div className="consolidate-btn">
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
      <div className="consolidate-content">
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

                      <TableCell>{Activity.absentDuration}</TableCell>
                      <TableCell>{Activity.days}</TableCell>
                      <TableCell>{Activity.status}</TableCell>
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
        <div className="user-consolidate-pagination">
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

export default UserConsolidate;
