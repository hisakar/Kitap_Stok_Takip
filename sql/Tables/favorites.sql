--------------------------------------------------------
--  File created - �ar�amba-Ocak-20-2021   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table FAVORITES
--------------------------------------------------------

  CREATE TABLE "SYSTEM"."FAVORITES" 
   (	"MEMBER_ID" NUMBER, 
	"BOOK_ID" NUMBER
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into SYSTEM.FAVORITES
SET DEFINE OFF;

--------------------------------------------------------
--  DDL for Index U_F
--------------------------------------------------------

  CREATE UNIQUE INDEX "SYSTEM"."U_F" ON "SYSTEM"."FAVORITES" ("MEMBER_ID", "BOOK_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  Constraints for Table FAVORITES
--------------------------------------------------------

  ALTER TABLE "SYSTEM"."FAVORITES" MODIFY ("MEMBER_ID" NOT NULL ENABLE);
  ALTER TABLE "SYSTEM"."FAVORITES" ADD CONSTRAINT "U_F" UNIQUE ("MEMBER_ID", "BOOK_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "SYSTEM"."FAVORITES" MODIFY ("BOOK_ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table FAVORITES
--------------------------------------------------------

  ALTER TABLE "SYSTEM"."FAVORITES" ADD CONSTRAINT "FK_BOOK_FAV" FOREIGN KEY ("BOOK_ID")
	  REFERENCES "SYSTEM"."BOOKS" ("BOOK_ID") ENABLE;
  ALTER TABLE "SYSTEM"."FAVORITES" ADD CONSTRAINT "FK_MEMBER_FAV" FOREIGN KEY ("MEMBER_ID")
	  REFERENCES "SYSTEM"."MEMBERS" ("MEMBER_ID") ENABLE;