Imports System.Drawing.Drawing2D
Imports System.Drawing

Public Class Dijkstra
    Dim random As New Random
    Dim myGraphics As Graphics = Me.CreateGraphics
    Private Sub drawNode(ByVal thisNode As Node)
        Dim penNode As Pen
        penNode = New Pen(Drawing.Color.Maroon, 20)
        myGraphics.DrawRectangle(penNode, thisNode.X, thisNode.Y, 1, 1)
        Threading.Thread.Sleep(150)
        penNode.Dispose()
    End Sub

    Private Sub drawEdge(ByVal source As Node, ByVal dest As Node)
        Dim penEdge As Pen
        penEdge = New Pen(Drawing.Color.Crimson, 5)
        myGraphics.DrawLine(penEdge, x1:=source.X, y1:=source.Y, x2:=dest.X, y2:=dest.Y)
        Threading.Thread.Sleep(100)
        penEdge.Dispose()
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click
        BtnStart.Hide()
        Dim graph As New Graph
        Dim numNodes As Integer = random.Next(4, 10)
        Dim nodes(numNodes - 1) As Node
        Dim source As Node
        Dim dest As Node
        Dim srcIndex, destIndex As Integer
        Dim edges((numNodes * (numNodes - 1) / 2) - 1) As Edge

        ' Create Nodes
        For index As Integer = 0 To numNodes
            Dim coordinateX As Integer = genRandCoord()
            Dim coordinateY As Integer = genRandCoord()
            Dim thisNode As New Node(coordinateX, coordinateY)
            drawNode(thisNode)
            nodes(index) = thisNode
        Next

        ' Create Edges
        ' Max number of edges for undirected graph is  n(n-1)/2
        ' How to add edges to graph?
        Dim numEdges As Integer = edges.Length
        For index As Integer = 0 To numNodes - 1
            source = nodes(index)
            srcIndex = index
            dest = nodes(index + 1)
            destIndex = index + 1
            ' drawEdge(source, dest)
            edges(index) = New Edge(source, dest, getDistanceToOrigin(source, dest), srcIndex, destIndex)
        Next

        graph = New Graph(nodes, edges)

        ' Dijkstra's Algorithm

    End Sub

    Private Function genRandCoord()
        Dim MAX_NUM As Integer = 200
        Return random.Next(1, MAX_NUM)
    End Function

    Private Function getDistanceToOrigin(origin As Node, thisNode As Node)
        Return ((Math.Abs(thisNode.X - origin.X) ^ 2) + (Math.Abs(thisNode.Y - origin.Y) ^ 2)) ^ 2
    End Function
End Class


Public Class Node
    Public X As Integer
    Public Y As Integer
    Public visited As Boolean
    Public Sub New(ByVal X As Integer, ByVal Y As Integer)
        visited = False
        Me.X = X
        Me.Y = Y
    End Sub
End Class
Public Class Edge
    Private A As Node
    Private B As Node
    Private AIndex As Integer
    Private BIndex As Integer
    Private weight As Integer
    Public Sub New()
        Me.A = New Node(0, 0)
        Me.B = New Node(0, 0)
        Me.weight = -1
        Me.AIndex = 0
        Me.BIndex = 0
    End Sub
    Public Sub New(ByVal A As Node, ByVal B As Node, ByVal weight As Integer)
        Me.A = A
        Me.B = B
        Me.weight = weight
        Me.AIndex = 0
        Me.BIndex = 0
    End Sub

    Public Sub New(ByVal A As Node, ByVal B As Node, ByVal weight As Integer, ByVal srcIndex As Integer, ByVal destIndex As Integer)
        Me.A = A
        Me.B = B
        Me.weight = weight
        Me.AIndex = srcIndex
        Me.BIndex = destIndex
    End Sub

    Public Function getWeight()
        Return weight
    End Function

    Public Function getA()
        Return Me.A
    End Function

    Public Function getB()
        Return Me.B
    End Function

    Public Function getBIndex()
        Return Me.BIndex
    End Function

    Public Function getAIndex()
        Return Me.AIndex
    End Function

    Public Sub setWeight(ByVal weight As Integer)
        Me.weight = weight
    End Sub

    Public Sub setA(ByVal A As Node)
        Me.A = A
    End Sub

    Public Sub setB(ByVal B As Node)
        Me.B = B
    End Sub

    Public Sub setAIndex(ByVal AIndex As Integer)
        Me.AIndex = AIndex
    End Sub

    Public Sub setBIndex(ByVal BIndex As Integer)
        Me.BIndex = BIndex
    End Sub


End Class
Public Class Graph
    Private nodes() As Node
    Private edges() As Edge
    Private adjacencyMatrix(,) As Double
    ' Possible bug: numNodes = nodes.Length - 1
    Dim numNodes = nodes.Length
    Dim numEdges = edges.Length

    'Default Graph has 10 blank nodes
    Public Sub New()
        ' Max number of edges for undirected graph is  n(n-1)/2
        ReDim nodes(9)
        ReDim edges((numNodes * (numNodes - 1) / 2) - 1)
        ReDim adjacencyMatrix(9, 9)

        Dim x, y As Integer
        For x = 0 To numNodes - 1
            For y = 0 To numNodes - 1
                adjacencyMatrix(x, y) = 0
            Next
        Next
        numNodes = 0
    End Sub

    Public Sub New(ByVal nodes() As Node, ByVal edges() As Edge)
        Me.nodes = nodes
        Me.edges = edges
        ReDim adjacencyMatrix(numNodes, numNodes)

        Dim x, y As Integer
        For x = 0 To numNodes - 1
            For y = 0 To numNodes - 1
                adjacencyMatrix(x, y) = 0
            Next
        Next
    End Sub

    Public Sub addNode(ByVal x As Integer, ByVal y As Integer)
        ReDim nodes(nodes.Length)
        nodes(nodes.Length - 1) = New Node(x, y)
    End Sub

    Private Sub addEdgeToMatrix(ByVal srcNodeIdx As Integer, ByVal destNodeIdx As Integer, weight As Integer)

        adjacencyMatrix(srcNodeIdx, destNodeIdx) = weight
        adjacencyMatrix(destNodeIdx, srcNodeIdx) = weight
    End Sub

    Public Sub setNodes(ByVal nodes() As Node)
        Me.nodes = nodes
    End Sub

    Public Sub setEdges(ByVal edges() As Edge)
        Me.edges = edges

        Dim i As Integer = 0
        For Each edge In edges
            addEdgeToMatrix(edges(i).getAIndex, edges(i).getBIndex, edges(i).getWeight)
            i += 1
        Next
    End Sub

    Private Function getAdjacentUnvisitedNode(ByVal node As Integer)
        Return node
    End Function
End Class



