import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    common: {
      secondaryColor: "#2771B8", // Reference your root color variable
      // secondaryColor: "#c6595e", // Reference your root color variable
      primaryColor: "#F3F3F3", // Reference your root color variable
      danger: "#BB2124", // Reference your root color variable
    },
  },
});

export default theme;
