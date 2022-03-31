Imports System.Drawing.Drawing2D
Imports System.Drawing

Public Class Dijkstra
    Dim rand As New Random
    Dim myGraphics As Graphics = Me.CreateGraphics
    Private Sub DrawNode(ByVal thisNode As Node)
        Dim penNode As Pen
        penNode = New Pen(Drawing.Color.Maroon, 20)
        myGraphics.DrawRectangle(penNode, thisNode.X, thisNode.Y, 1, 1)
        Threading.Thread.Sleep(150)
        penNode.Dispose()
    End Sub

    Private Sub DrawEdge(ByVal thisEdge As Edge, ByVal color As Drawing.Color, ByVal width As Integer)
        Dim penEdge As Pen
        penEdge = New Pen(color, width)
        myGraphics.DrawLine(penEdge, x1:=thisEdge.getA().X, y1:=thisEdge.getA().Y, x2:=thisEdge.getB().X, y2:=thisEdge.getB().Y)
        Threading.Thread.Sleep(100)
        penEdge.Dispose()
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click
        BtnStart.Hide()
        Dim numNodes As Integer = rand.Next(4, 10)

        Dim randomEdgeGraph As New Graph(numNodes)

        For Each node In randomEdgeGraph.getNodes()
            drawNode(node)
        Next
        For Each edge In randomEdgeGraph.GetEdges()
            DrawEdge(edge, Color.Crimson, 2)
        Next

        ' Dijkstra's Algorithm




        For Each edge In randomEdgeGraph.GetEdges()
            'DrawEdge(edge, Color.CornflowerBlue, 4)
        Next
    End Sub




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
    Private weight As Double
    Public Sub New()
        Me.A = New Node(0, 0)
        Me.B = New Node(0, 0)
        Me.weight = -1
        Me.AIndex = 0
        Me.BIndex = 0
    End Sub
    Public Sub New(ByVal A As Node, ByVal B As Node, ByVal weight As Double)
        Me.A = A
        Me.B = B
        Me.weight = weight
        Me.AIndex = 0
        Me.BIndex = 0
    End Sub

    Public Sub New(ByVal A As Node, ByVal B As Node, ByVal weight As Double, ByVal srcIndex As Integer, ByVal destIndex As Integer)
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

    Public Sub setWeight(ByVal weight As Double)
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
    Dim rand As New Random
    Private nodes() As Node
    Private edges() As Edge
    Private adjacencyMatrix(,) As Double
    Dim numNodes = 0
    Dim numEdges = 0

    Public Sub New(ByVal numNodes As Integer)
        nodes = GenerateNodes(numNodes)
        ReDim adjacencyMatrix(numNodes - 1, numNodes - 1)
        Me.numNodes = nodes.Length

        Dim x, y As Integer
        For x = 0 To numNodes - 1
            For y = 0 To numNodes - 1
                adjacencyMatrix(x, y) = 0
            Next
        Next
        Dim maxEdges As Integer = (numNodes * (numNodes - 1) / 2)
        Dim minEdges As Integer = numNodes - 1
        numEdges = rand.Next(minEdges, maxEdges)
        ReDim edges(numEdges - 1)
        edges = GenerateEdges()
    End Sub

    Public Sub New(ByVal nodes As Node())
        Me.nodes = nodes
        numNodes = nodes.Length
        ReDim adjacencyMatrix(numNodes, numNodes)
        Me.numNodes = nodes.Length

        Dim x, y As Integer
        For x = 0 To numNodes - 1
            For y = 0 To numNodes - 1
                adjacencyMatrix(x, y) = 0
            Next
        Next
        Dim maxEdges As Integer = (numNodes * (numNodes - 1) / 2)
        Dim minEdges As Integer = numNodes - 1
        numEdges = rand.Next(minEdges, maxEdges)
        ReDim edges(numEdges - 1)
        edges = GenerateEdges()

    End Sub

    Public Sub New(ByVal nodes() As Node, ByVal edges() As Edge)
        Me.nodes = nodes
        Me.edges = edges
        ReDim adjacencyMatrix(numNodes, numNodes)
        numNodes = nodes.Length
        numEdges = edges.Length

        Dim x, y As Integer
        For x = 0 To numNodes - 1
            For y = 0 To numNodes - 1
                adjacencyMatrix(x, y) = 0
            Next
        Next
    End Sub
    Public Function GenerateNodes(numNodes As Integer) As Node()
        Dim nodes(numNodes - 1) As Node
        Dim CoordinateMax As Integer = 200
        For index As Integer = 0 To numNodes - 1
            Dim coordinateX As Integer = GenRandCoord(CoordinateMax)
            Dim coordinateY As Integer = GenRandCoord(CoordinateMax)
            Dim thisNode As New Node(coordinateX, coordinateY)
            nodes(index) = thisNode
        Next
        Return nodes
    End Function
    Public Function GenerateEdges() As Edge()
        Dim sourceNodeIdx, destNodeIdx As Integer
        Dim src, dest As Node
        Dim nodeIndices As List(Of Integer)

        'Something is wrong
        ' once: 6 nodes 1 edge
        For i As Integer = 0 To numEdges - 1
            nodeIndices = Enumerable.Range(0, numNodes).ToList()
            sourceNodeIdx = rand.Next(0, nodeIndices.Count)
            nodeIndices.RemoveAt(sourceNodeIdx)
            destNodeIdx = rand.Next(0, nodeIndices.Count)
            nodeIndices.RemoveAt(destNodeIdx)
            src = nodes(sourceNodeIdx)
            dest = nodes(destNodeIdx)
            edges(i) = New Edge(src, dest, GetDistanceToOrigin(src, dest), sourceNodeIdx, destNodeIdx)
            AddEdgeToMatrix(edges(i))
        Next

        Return edges
    End Function
    Public Sub AddNode(ByVal x As Integer, ByVal y As Integer)
        ReDim nodes(nodes.Length)
        nodes(nodes.Length - 1) = New Node(x, y)
    End Sub

    Private Sub AddEdgeToMatrix(edge As Edge)
        adjacencyMatrix(edge.getAIndex, edge.getBIndex) = 1
        adjacencyMatrix(edge.getBIndex, edge.getAIndex) = 1
    End Sub

    Private Function GenRandCoord(ByVal MAX_NUM As Integer)
        Return rand.Next(1, MAX_NUM)
    End Function

    Private Function GetDistanceToOrigin(origin As Node, thisNode As Node)
        Return (((Math.Abs(thisNode.X - origin.X) ^ 2) + (Math.Abs(thisNode.Y - origin.Y) ^ 2)) ^ 2)
    End Function

    Private Function GetAdjacentUnvisitedNode(ByVal node As Node)
        Return node
    End Function

    Friend Function GetEdges() As IEnumerable(Of Object)
        Return edges
    End Function

    Friend Function GetNodes() As IEnumerable(Of Object)
        Return nodes
    End Function
End Class



