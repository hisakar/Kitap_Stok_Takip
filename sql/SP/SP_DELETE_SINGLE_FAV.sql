create or replace NONEDITIONABLE PROCEDURE SP_DELETE_SINGLE_FAV
(
  v_MEMBER_ID IN NUMBER,
  v_BOOK_ID IN NUMBER
)
AS

BEGIN
   DELETE FAVORITES
    WHERE  MEMBER_ID = v_MEMBER_ID AND BOOK_ID = v_BOOK_ID;

END;