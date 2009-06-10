create or replace package body PROV_TEST_STANDA is

  -- Function and procedure implementations
  procedure UpdateResponseToFtPro (  
  p_REQ_ID foris_commands_prov.req_id%type, 
	p_PROCESSING_STATUS in VARCHAR2, 
	p_UPDATE_MECHANISM in VARCHAR2, 
	p_RESPONSE_TIME in DATE, 
	p_ERROR_DESC in CLOB, 
	p_PREPAID_CREDIT in VARCHAR2,
  p_Updated out boolean)
  
  is
  rowcnt     number := 0;

  begin
  
  p_Updated := true;

  MERGE INTO frs_commands_rsp_log_p_standa l
  USING (SELECT p_REQ_ID as "REQ_ID", p_PROCESSING_STATUS as "PROCESSING_STATUS", 
        p_UPDATE_MECHANISM as "UPDATE_MECHANISM", p_RESPONSE_TIME as "RESPONSE_TIME", 
        p_ERROR_DESC as "ERROR_DESC", p_PREPAID_CREDIT as "PREPAID_CREDIT" FROM dual) r
  ON (l.REQ_ID = r.REQ_ID)
  WHEN MATCHED THEN
  UPDATE 
       SET l.PROCESSING_STATUS = r.PROCESSING_STATUS,
        l.UPDATE_MECHANISM = r.UPDATE_MECHANISM,
        l.RESPONSE_TIME = r.RESPONSE_TIME,
        l.ERROR_DESC = r.ERROR_DESC,
        l.PREPAID_CREDIT = r.PREPAID_CREDIT
       
  WHEN NOT MATCHED THEN
  INSERT (REQ_ID, PROCESSING_STATUS, UPDATE_MECHANISM, RESPONSE_TIME, ERROR_DESC, PREPAID_CREDIT)
  VALUES (r.REQ_ID, r.PROCESSING_STATUS, r.UPDATE_MECHANISM, r.RESPONSE_TIME, r.ERROR_DESC, r.PREPAID_CREDIT);
  
  SAVEPOINT log;

  UPDATE foris_commands_prov_standa c SET c.status = p_PROCESSING_STATUS WHERE c.req_id = p_REQ_ID;
  
  rowcnt := SQL%ROWCOUNT;

  commit;
  
  IF rowcnt = 0 THEN
  --raise_application_error(-20999, 'REQ_ID:' || p_REQ_ID || '-' || 'No command with such id!' ); 
   p_Updated := false;
  END IF;
  exception
   WHEN OTHERS THEN
      rollback to log;
      raise; 
  
  end UpdateResponseToFtPro;

--begin
  -- Initialization
--  null;
end PROV_TEST_STANDA;
