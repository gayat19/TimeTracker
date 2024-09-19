import { Workbook } from "exceljs";
import { workingHours } from "./admin.config";
import { excelTypeAll } from "./contants";

const toDataURL = async (url) => {
  try {
    const response = await fetch(url);
    const blob = await response.blob();
    const reader = new FileReader();
    const promise = new Promise((resolve, reject) => {
      reader.onloadend = () => resolve({ base64Url: reader.result });
      reader.onerror = reject;
    });
    reader.readAsDataURL(blob);
    return promise;
  } catch (error) {
    // Handle image fetching errors gracefully (e.g., display a placeholder)
  }
};

export const exportDataToExcel = async (
  columns,
  data,
  type,
  fileName = "download.xlsx"
) => {
  try {
    const workbook = new Workbook();
    const sheet = workbook.addWorksheet(fileName);
    sheet.properties.defaultRowHeight = 80;

    // Header Row Formatting
    const headerRow = sheet.getRow(1);

    headerRow.fill = {
      type: "pattern",
      pattern: "solid",
      fgColor: { argb: "FFFFCC00" }, // Yellow background color
    };

    headerRow.border = {
      top: { style: "thin", color: { argb: "000000" } }, // Black
      bottom: { style: "thin", color: { argb: "000000" } }, // Black
      left: { style: "thin", color: { argb: "000000" } }, // Black
      right: { style: "thin", color: { argb: "000000" } }, // Black
    };
    headerRow.font = {
      name: "Calibri", // Modern font (optional)
      family: 4,
      size: 14,
      bold: true,
    };
    // Column Definitions

    sheet.columns = columns;

    // Add Data Rows
    for (const [index, product] of data.entries()) {
      const rowData = { ...product };
      if (type === excelTypeAll) {
        rowData.status =
          product.durationInMins > workingHours ? "PRESENT" : "ABSENT";
      }
      rowData.sno = index + 1;
      sheet.addRow(rowData); // Clone product object to avoid mutations
    }

    if (type === excelTypeAll) {
      const statusCol = sheet.getColumn("status"); // Get the "status" column
      statusCol.eachCell((cell, rowNumber) => {
        if (rowNumber > 1) {
          // Skip header row
          const cellValue = sheet.getCell(cell.address).value;
          if (cellValue === "Absent") {
            cell.fill = {
              type: "pattern",
              pattern: "solid",
              fgColor: { argb: "FF0000" },
            }; // Red for Absent
          } else if (cellValue === "Present") {
            cell.fill = {
              type: "pattern",
              pattern: "solid",
              fgColor: { argb: "00FF00" },
            }; // Green for Present
          }
        }
      });
    }

    const buffer = await workbook.xlsx.writeBuffer();
    const blob = new Blob([buffer], {
      type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
    });
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement("a");
    link.href = url;
    link.download = fileName;
    link.click();
    window.URL.revokeObjectURL(url);
  } catch (error) {
    // Handle export errors gracefully (e.g., display an error message)
  }
};
