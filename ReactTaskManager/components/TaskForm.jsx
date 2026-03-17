import { useState } from 'react'

export default function TaskForm({ addTask }) {
    const [text, setText] = useState("");

    return (
        <div className="form-div">
            <input className="text-input" type="text" value={text} onChange={(e) => setText(e.target.value)} onKeyDown={(e) => { if (e.key === "Enter") { addTask(text); setText("") } }} />
            <button className="button" onClick={() => { addTask(text); setText("") }}>Dodaj</button>
        </div>
    );
}