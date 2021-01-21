create or replace NONEDITIONABLE PROCEDURE SP_REGISTER_USER
(
  v_Username IN NVARCHAR2,
  v_Password IN NVARCHAR2,
  v_Email IN NVARCHAR2
)
AS
   v_temp NUMBER(1, 0) := 0;
   v_cursor SYS_REFCURSOR;

BEGIN
   BEGIN
      SELECT 1 INTO v_temp
        FROM DUAL
       WHERE EXISTS ( SELECT MEMBER_ID 
                      FROM MEMBERS 
                       WHERE  USER_NAME = v_Username );
   EXCEPTION
      WHEN OTHERS THEN
         NULL;
   END;

   IF v_temp = 1 THEN

   BEGIN
      OPEN  v_cursor FOR
         SELECT -1 -- Username exists.
           FROM DUAL  ;
         DBMS_SQL.RETURN_RESULT(v_cursor);

   END;
   ELSE
      BEGIN
         SELECT 1 INTO v_temp
           FROM DUAL
          WHERE EXISTS ( SELECT MEMBER_ID 
                         FROM MEMBERS 
                          WHERE  EMAIL = v_Email );
      EXCEPTION
         WHEN OTHERS THEN
            NULL;
      END;

      IF v_temp = 1 THEN

      BEGIN
         OPEN  v_cursor FOR
            SELECT -2 -- Email exists.
              FROM DUAL  ;
            DBMS_SQL.RETURN_RESULT(v_cursor);

      END;
      ELSE

      BEGIN
         INSERT INTO MEMBERS
           ( USER_NAME, PASSWORD, EMAIL, ROLE )
           VALUES ( v_Username, v_Password, v_Email, 'User' );
         OPEN  v_cursor FOR
            SELECT MEMBER_ID FROM MEMBERS WHERE USER_NAME = v_Username; --MEMBER_ID
                        DBMS_SQL.RETURN_RESULT(v_cursor);

      END;
      END IF;
   END IF;

--EXCEPTION WHEN OTHERS THEN utils.handleerror(SQLCODE,SQLERRM);
END;