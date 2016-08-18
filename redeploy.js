//@url('/redeploy') 
//@required('token', 'targetAppid', 'nodeGroup')

return jelastic.env.control.RestartContainersByGroup(targetAppid, signature, nodeGroup);
