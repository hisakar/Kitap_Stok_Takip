create or replace NONEDITIONABLE PROCEDURE SP_INSERT_FAV
    (
      v_MEMBER_ID IN NUMBER,
      v_BOOK_ID IN NUMBER 
    )
    AS
    
    BEGIN
       INSERT INTO FAVORITES
         VALUES ( v_MEMBER_ID, v_BOOK_ID );
    END;