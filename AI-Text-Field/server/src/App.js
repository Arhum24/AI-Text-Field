import './App.css';
import { Box, TextField } from '@mui/material';

function App() {
  return (
    <Box
      component="form"
      sx={{ '& .MuiTextField-root': { m: 1, width: '25ch' } }}
      noValidate
      autoComplete="off"
    >
      <TextField
        id="standard-multiline-static"
        label="Start Typing"
        multiline
        rows={4}
        variant="standard"
        sx={{border:"static"}}
      />
    </Box>
  );
}

export default App;
