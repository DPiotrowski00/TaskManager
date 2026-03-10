export default function TaskList({ tasks, onCompletedClick, onDeleteClick }) {
    const taskMap = tasks.map((task) => {
        return (
            <tr key={task.id}>
                <td>{task.id}</td>
                <td>{task.text}</td>
                <td><input type="checkbox" checked={task.completed} onChange={() => onCompletedClick(task.id)}></input></td>
                <td>{task.createdAt}</td>
                <td>{task.creator}</td>
                <td><button onClick={() => onDeleteClick(task.id)}>X</button></td>
            </tr>
        );
    });

    return (
        <div>
            <table className="task-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Tekst</th>
                        <th>Zakończono</th>
                        <th>Data dodania</th>
                        <th>Utworzył</th>
                        <th>Usuń</th>
                    </tr>
                </thead>
                <tbody>
                    {taskMap}
                </tbody>
            </table>
        </div>
    );
}