import './App.css';
import { Box, TextField, Button, Typography } from '@mui/material';
import { useState, useEffect } from 'react';

function App() {
  const [animate, setAnimate] = useState(false);
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    if (animate) {
      // Reset animation after 3 seconds
      const timer = setTimeout(() => {
        setSuccess(true); // Show Success message
        setTimeout(() => setSuccess(false), 2000); // Hide Success message after 2 seconds
        setAnimate(false); // Reset the animation state
      }, 3000); // Wait for the animation duration
      return () => clearTimeout(timer);
    }
  }, [animate]);

  const handleClick = () => {
    setAnimate(true); // Trigger animation on button click
  };

  return (
    <Box
      component="form"
      sx={{
        display: 'flex',
        alignItems: 'flex-start',
        gap: 2,
        p: 3,
      }}
      noValidate
      autoComplete="off"
    >
      <Box sx={{ position: 'relative' }}>
        {/* TextField */}
        <TextField
          id="standard-multiline-static"
          label="Start Typing"
          multiline
          rows={6}
          variant="outlined"
          sx={{
            '& .MuiOutlinedInput-root': {
              borderRadius: '16px',
              width: '50ch',
              position: 'relative',
              transition: 'border-color 2s ease-in-out', // Smooth border transition
              borderColor: animate || success ? '#6EC1E4' : 'rgba(0, 0, 0, 0.23)', // Blue border on success or animation
            },
          }}
        />

        {/* Conditional Diagonal overlay with smooth opacity transition */}
        <Box
          sx={{
            position: 'absolute',
            top: 0,
            right: 0,
            bottom: 0,
            left: '50%',
            background: '#6EC1E4', // Slightly darker blue than the previous baby blue
            clipPath: 'polygon(100% 0, 0 100%, 100% 100%)', // Diagonal from top-right to bottom-left
            borderRadius: "16px",
            zIndex: 1, // Ensures overlay is above the text field
            opacity: animate ? 1 : 0, // Smooth opacity transition
            transform: animate ? 'scale(1)' : 'scale(0.5)', // Slight scale effect
            transition: 'opacity 2s ease-in-out, transform 2s ease-in-out', // Smooth, slower transition
          }}
        />

        {/* Success Message */}
        {success && (
          <Typography
            variant="h6"
            sx={{
              position: 'absolute',
              top: -27,
              right: 10,
              color: '#4285F4', // Success message in blue
              opacity: success ? 1 : 0,
              transition: 'opacity 1s ease-in-out',
            }}
          >
            Success
          </Typography>
        )}
      </Box>

      <Button
        variant="contained"
        color="primary"
        sx={{
          px: 2,
          py: 1,
          borderRadius: '50px',
          alignSelf: 'end',
        }}
        onClick={handleClick}
      >
        Generate
      </Button>
    </Box>
  );
}

export default App;
