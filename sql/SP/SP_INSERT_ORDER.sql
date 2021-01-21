create or replace NONEDITIONABLE PROCEDURE SP_INSERT_ORDER
(
  v_MEMBER_ID IN NUMBER,
  v_BOOK_ID IN NUMBER 
)
AS

BEGIN
   INSERT INTO ORDERS
     VALUES ( v_MEMBER_ID, v_BOOK_ID, TO_CHAR(sysdate,'DD/MM/YYYY HH24:MI:SS') );
END;