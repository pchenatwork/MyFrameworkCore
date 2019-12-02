select * into WorkflowNode1 FROM WorkflowNode
drop table WorkflowNode

CREATE TABLE [dbo].[WorkflowNode] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [WorkflowId]        INT           NULL,
    [Name]              VARCHAR (100) NOT NULL,
    [Description]       VARCHAR (250) NULL,
    [NodeTypeEnum]      INT           NOT NULL,
    [NodeFromId]        INT           NOT NULL,
    [NodeToId]          INT           NOT NULL,
    [StepId]            INT           NULL,
    [ActionName]        VARCHAR (100) NULL,
    [IsPermissioned]    CHAR (1)      NOT NULL,
    [TimeToSkip]        INT           NOT NULL,
    [NodeValue]         VARCHAR (50)  NULL,
    [NodeConditionEnum] INT           NOT NULL,
    [IsAuto]            CHAR (1)      NULL
);

INSERT INTO [dbo].[WorkflowNode] ([WorkflowId], [Name], [Description], [NodeTypeEnum], [NodeFromId], [NodeToId], [StepId], [ActionName], [IsPermissioned], [TimeToSkip], [NodeValue], [NodeConditionEnum], [IsAuto]) 
SELECT [WorkflowId], [Name], [Description], [NodeTypeEnum], [NodeFromId], [NodeToId], [StepId], [ActionName], [IsPermissioned], [TimeToSkip], [NodeValue], [NodeConditionEnum], [IsAuto]
FROM WorkflowNode1

DROP TABLE WorkflowNode1

select * from WorkflowNode