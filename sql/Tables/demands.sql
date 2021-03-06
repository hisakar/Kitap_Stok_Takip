--------------------------------------------------------
--  File created - �ar�amba-Ocak-20-2021   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table DEMANDS
--------------------------------------------------------

  CREATE TABLE "SYSTEM"."DEMANDS" 
   (	"DEMAND_ID" NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"MEMBER_ID" NUMBER, 
	"BOOK_NAME" VARCHAR2(50 BYTE), 
	"WRITER" VARCHAR2(50 BYTE), 
	"REQUESTED_DATE" VARCHAR2(20 BYTE)
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into SYSTEM.DEMANDS
SET DEFINE OFF;

--------------------------------------------------------
--  DDL for Index SYS_C009936
--------------------------------------------------------

  CREATE UNIQUE INDEX "SYSTEM"."SYS_C009936" ON "SYSTEM"."DEMANDS" ("DEMAND_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  Constraints for Table DEMANDS
--------------------------------------------------------

  ALTER TABLE "SYSTEM"."DEMANDS" MODIFY ("DEMAND_ID" NOT NULL ENABLE);
  ALTER TABLE "SYSTEM"."DEMANDS" MODIFY ("MEMBER_ID" NOT NULL ENABLE);
  ALTER TABLE "SYSTEM"."DEMANDS" MODIFY ("BOOK_NAME" NOT NULL ENABLE);
  ALTER TABLE "SYSTEM"."DEMANDS" MODIFY ("WRITER" NOT NULL ENABLE);
  ALTER TABLE "SYSTEM"."DEMANDS" ADD PRIMARY KEY ("DEMAND_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "SYSTEM"."DEMANDS" MODIFY ("REQUESTED_DATE" NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table DEMANDS
--------------------------------------------------------

  ALTER TABLE "SYSTEM"."DEMANDS" ADD CONSTRAINT "FK_MEMBER_DEMAND" FOREIGN KEY ("MEMBER_ID")
	  REFERENCES "SYSTEM"."MEMBERS" ("MEMBER_ID") ENABLE;
