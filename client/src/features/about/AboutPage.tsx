import { Alert, AlertTitle, Button, ButtonGroup, Container, List, ListItem, ListItemText, Typography } from '@mui/material'
import React, { useState } from 'react'
import agent from '../../app/api/agent'
import { error } from 'console';

function AboutPage() {

  const [validationErrors, setValidationErrors] = useState<string[]>([]);

  function getValidationError(){
    agent.TestError.getValidationError()
      .then(()=>console.log('should not see this'))
      .catch(error=>setValidationErrors(error));
  }

  return (
    <Container>

    <Typography gutterBottom variant='h2'>Errors for testing purposes</Typography>
    <ButtonGroup fullWidth>
      <Button variant='contained' onClick={()=>agent.TestError.get400Error().catch(err=>console.log(err))}>Test 400 Error</Button>
      <Button variant='contained' onClick={()=>agent.TestError.get401Error().catch(err=>console.log(err))}>Test 401 Error</Button>
      <Button variant='contained' onClick={()=>agent.TestError.get404Error().catch(err=>console.log(err))}>Test 404 Error</Button>
      <Button variant='contained' onClick={()=>agent.TestError.get500Error().catch(err=>console.log(err))}>Test 500 Error</Button>
      <Button variant='contained' onClick={getValidationError}>Test Validation Error</Button>
    </ButtonGroup>
    {
      validationErrors.length>0 &&
      <Alert severity='error'>
        <AlertTitle>Validation Errors</AlertTitle>
        <List>
          {validationErrors.map(error=>(
            <ListItem key={error}>
              <ListItemText>{error}</ListItemText>
            </ListItem>
          ))}
        </List>
      </Alert>
    }
    </Container>
  )
}

export default AboutPage