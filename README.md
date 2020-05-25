# liquibase-editor

O objetivo do projeto é facilitar a geração de scripts Liquibase utilizando o banco de dados como referência;

A aplicação se conecta atualmente apenas em bases SQL Server ou Oracle e realiza a engenharia reversa, transformando tabelas, colunas, chaves primarias e chaves estrangeiras em scripts Liquibase;

Com o passar do tempo o objetivo é expandir para a utilização de outras bases de dados como por exemplo PostgreSQL e MySQL;

## Script de criação da tabela:

É necessário primeiro criar as tabelas que se deseja obter o script Liquibase na base de dados

```sql
CREATE TABLE [dbo].[GA_TIMESHEET](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PRODUCTION_UNIT_ID] [bigint] NOT NULL,
	[DIVISION_ID] [bigint] NOT NULL,
	[REGION_ID] [bigint] NOT NULL,
	[APPOINTMENT_DATE] [datetime] NOT NULL,
	[SYS_USER_NAME_CREATOR] [varchar](100) NULL,
	[SYS_USER_NAME_LAST_CHANGE] [varchar](100) NULL,
	[SYS_CREATION_DATE] [datetime] NULL,
	[SYS_LAST_CHANGE_DATE] [datetime] NULL,
	[ACTIVE] [bit] NOT NULL,
	[INACTIVE_DATE] [datetime] NULL,
	[Lorem] [real] NULL,
 CONSTRAINT [PK_GA_TIMESHEET] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, 
 STATISTICS_NORECOMPUTE = OFF,
 IGNORE_DUP_KEY = OFF,
 ALLOW_ROW_LOCKS = ON,
 ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GA_TIMESHEET] 
  ADD  CONSTRAINT [DF_GA_TIMESHEET_ACTIVE]  DEFAULT ((1)) FOR [ACTIVE]
GO

ALTER TABLE [dbo].[GA_TIMESHEET]  WITH CHECK 
  ADD  CONSTRAINT [FK_TIMESHEET_DIVISION] FOREIGN KEY([DIVISION_ID])
REFERENCES [dbo].[GA_LAYERS] ([ID])
GO

ALTER TABLE [dbo].[GA_TIMESHEET] CHECK CONSTRAINT [FK_TIMESHEET_DIVISION]
GO

ALTER TABLE [dbo].[GA_TIMESHEET]  WITH CHECK 
  ADD  CONSTRAINT [FK_TIMESHEET_PROD_UNIT] FOREIGN KEY([PRODUCTION_UNIT_ID])
REFERENCES [dbo].[GA_PRODUCTION_UNIT] ([ID])
GO

ALTER TABLE [dbo].[GA_TIMESHEET] CHECK CONSTRAINT [FK_TIMESHEET_PROD_UNIT]
GO

ALTER TABLE [dbo].[GA_TIMESHEET]  WITH CHECK 
  ADD  CONSTRAINT [FK_TIMESHEET_REGION] FOREIGN KEY([REGION_ID])
REFERENCES [dbo].[GA_LAYERS] ([ID])
GO

ALTER TABLE [dbo].[GA_TIMESHEET] CHECK CONSTRAINT [FK_TIMESHEET_REGION]
GO
```

## Interface do programa:

Script Liquibase resultante:

```xml
<?xml version="1.0" encoding="utf-16"?>
<changeSet id="20200524-1" author="altair.sossai">
  <preConditions onFail="MARK_RAN">
    <not>
      <tableExists tableName="GA_TIMESHEET" />
    </not>
  </preConditions>
  <createTable remarks="GA_TIMESHEET" schemaName="${dbSchemaName}" tableName="GA_TIMESHEET">
    <column name="ID" type="${id_type}" autoIncrement="${autoIncrement}" remarks="ID">
      <constraints primaryKey="true" primaryKeyName="PK_GA_TIMESHEET" nullable="false" />
    </column>
    <column name="PRODUCTION_UNIT_ID" type="${id_type}" remarks="PRODUCTION_UNIT_ID">
      <constraints nullable="false" />
    </column>
    <column name="DIVISION_ID" type="${id_type}" remarks="DIVISION_ID">
      <constraints nullable="false" />
    </column>
    <column name="REGION_ID" type="${id_type}" remarks="REGION_ID">
      <constraints nullable="false" />
    </column>
    <column name="APPOINTMENT_DATE" type="${datetime_type}" remarks="APPOINTMENT_DATE">
      <constraints nullable="false" />
    </column>
    <column name="SYS_USER_NAME_CREATOR" type="VARCHAR(100 BYTE)" remarks="SYS_USER_NAME_CREATOR">
      <constraints nullable="true" />
    </column>
    <column name="SYS_USER_NAME_LAST_CHANGE" type="VARCHAR(100 BYTE)" remarks="SYS_USER_NAME_LAST_CHANGE">
      <constraints nullable="true" />
    </column>
    <column name="SYS_CREATION_DATE" type="${datetime_type}" remarks="SYS_CREATION_DATE">
      <constraints nullable="true" />
    </column>
    <column name="SYS_LAST_CHANGE_DATE" type="${datetime_type}" remarks="SYS_LAST_CHANGE_DATE">
      <constraints nullable="true" />
    </column>
    <column name="ACTIVE" type="${boolean_type}" remarks="ACTIVE">
      <constraints nullable="false" />
    </column>
    <column name="INACTIVE_DATE" type="${datetime_type}" remarks="INACTIVE_DATE">
      <constraints nullable="true" />
    </column>
    <column name="Lorem" type="NUMBER(24, 0)" remarks="Lorem">
      <constraints nullable="true" />
    </column>
  </createTable>
</changeSet>

<?xml version="1.0" encoding="utf-16"?>
<changeSet id="20200524-2" dbms="oracle" author="altair.sossai">
  <preConditions onFail="MARK_RAN">
    <sqlCheck expectedResult="0">SELECT COUNT(*) FROM USER_SEQUENCES U WHERE UPPER(U.SEQUENCE_NAME) = UPPER('SEQ_GA_TIMESHEET')</sqlCheck>
  </preConditions>
  <createSequence cycle="true" incrementBy="1" maxValue="9999999999999" minValue="1" ordered="true" schemaName="${dbSchemaName}" sequenceName="SEQ_GA_TIMESHEET" startValue="1" />
</changeSet>

<?xml version="1.0" encoding="utf-16"?>
<changeSet id="20200524-3" author="altair.sossai">
  <preConditions onFail="MARK_RAN">
    <not>
      <foreignKeyConstraintExists foreignKeyName="FK_TIMESHEET_PROD_UNIT" />
    </not>
  </preConditions>
  <addForeignKeyConstraint baseColumnNames="PRODUCTION_UNIT_ID" baseTableName="GA_TIMESHEET" constraintName="FK_TIMESHEET_PROD_UNIT" referencedTableName="GA_PRODUCTION_UNIT" referencedColumnNames="ID" deferrable="false" initiallyDeferred="false" onDelete="NO ACTION" onUpdate="NO ACTION" />
</changeSet>

<?xml version="1.0" encoding="utf-16"?>
<changeSet id="20200524-4" author="altair.sossai">
  <preConditions onFail="MARK_RAN">
    <not>
      <foreignKeyConstraintExists foreignKeyName="FK_TIMESHEET_DIVISION" />
    </not>
  </preConditions>
  <addForeignKeyConstraint baseColumnNames="DIVISION_ID" baseTableName="GA_TIMESHEET" constraintName="FK_TIMESHEET_DIVISION" referencedTableName="GA_LAYERS" referencedColumnNames="ID" deferrable="false" initiallyDeferred="false" onDelete="NO ACTION" onUpdate="NO ACTION" />
</changeSet>

<?xml version="1.0" encoding="utf-16"?>
<changeSet id="20200524-5" author="altair.sossai">
  <preConditions onFail="MARK_RAN">
    <not>
      <foreignKeyConstraintExists foreignKeyName="FK_TIMESHEET_REGION" />
    </not>
  </preConditions>
  <addForeignKeyConstraint baseColumnNames="REGION_ID" baseTableName="GA_TIMESHEET" constraintName="FK_TIMESHEET_REGION" referencedTableName="GA_LAYERS" referencedColumnNames="ID" deferrable="false" initiallyDeferred="false" onDelete="NO ACTION" onUpdate="NO ACTION" />
</changeSet>
```
