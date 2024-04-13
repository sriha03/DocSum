import React, { useState } from 'react';
import axios from 'axios';

function App() {
    const [summary, setSummary] = useState('');
    const [file, setFile] = useState(null);

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const handleSummarize = async () => {
        if (!file) {
            alert('Please select a file.');
            return;
        }

        const formData = new FormData();
        formData.append('file', file);

        try {
            const response = await axios.post('/summarize', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            setSummary(response.data.summary);
        } catch (error) {
            console.error('Error:', error);
        }
    };

    return (
        <div>
            <h1>Document Summarizer</h1>
            <input type="file" onChange={handleFileChange} />
            <button onClick={handleSummarize}>Summarize</button>
            <div>{summary}</div>
        </div>
    );
}

export default App;
